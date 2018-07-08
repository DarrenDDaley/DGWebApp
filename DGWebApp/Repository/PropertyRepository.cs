using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DGWebApp.Models.Post;
using DGWebApp.Models.Put;
using DGWebApp.Settings;
using Microsoft.Extensions.Options;

namespace DGWebApp.Repository
{
    public class PropertyRepository : IProperyRepositroy
    {
        private readonly IDbConnection db;
        private readonly IOptions<ConnectionStrings> connectionStrings;

        public PropertyRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));

            db = new SqlConnection(this.connectionStrings.Value.Default);
        }

        public async Task<int> Insert(PostProperty property)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string insertQuery = "INSERT INTO dbo.Properties (Housenumber, Street, Town, PostCode, AvailableFrom,  Status, LandlordId)" +
                                         "VALUES(@Housenumber, @Street, @Town, @PostCode, @AvailableFrom, @Status, @LandlordId);" +
                                         "SELECT CAST(SCOPE_IDENTITY() AS int)";

                    dbConnection.Open();
                    var idCollection = await dbConnection.QueryAsync<int>(insertQuery, property);
                    return idCollection.FirstOrDefault();
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task<PutProperty> Select(int id)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string selectQuery = $"SELECT * FROM dbo.Properties WHERE PropertyId = { id }";

                    dbConnection.Open();
                    var property = await dbConnection.QueryAsync<PutProperty>(selectQuery);
                    return property.First();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PutProperty>> SelectAddress(string address)
        {
            try
            {
                IEnumerable<PutProperty> properties;

                using (IDbConnection dbConnection = db)
                {
                    dbConnection.Open();

                    string selectQuery = $"SELECT * FROM dbo.Properties WHERE Street = @Address OR TOWN = @Address OR PostCode = @Address";
                    properties = await dbConnection.QueryAsync<PutProperty>(selectQuery, new { Address = address });
                    return properties;
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> Update(int id, PutProperty property)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    var result = 0;
                    var updateFields = UpdateStringConstruction(property);

                    dbConnection.Open();

                    foreach (var updateField in updateFields)
                    {

                        string updateQuery = $"UPDATE dbo.Properties " +
                                             $"SET { updateField }" +
                                             $"WHERE PropertyId = { id }";


                        result = await dbConnection.ExecuteAsync(updateQuery, property);
                    }
                    return result > 0 ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<string> UpdateStringConstruction(PutProperty property)
        {
            List<string> fields = new List<string>();

            if (!string.IsNullOrWhiteSpace(property.Housenumber)) {
                fields.Add("Forename = @Housenumber ");
            }

            if (!string.IsNullOrWhiteSpace(property.Street))
            {
                fields.Add("Surname = @Street ");
            }

            if (!string.IsNullOrWhiteSpace(property.Town)) {
                fields.Add("Email = @Town ");
            }

            if (!string.IsNullOrWhiteSpace(property.PostCode)) {
                fields.Add("PostCode = @PostCode ");
            }

            if (property.AvailableFrom != null) {
                fields.Add("AvailableFrom = @AvailableFrom ");
            }

            if (!string.IsNullOrWhiteSpace(property.Status)) {
                fields.Add("Status = @Status ");
            }

            if (property.LandlordId > 0) {
                fields.Add("Phone = @LandlordId ");
            }

            return fields.ToArray();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string deleteQuery = $"DELETE FROM dbo.Properties WHERE PropertyId = { id }";

                    dbConnection.Open();
                    var result = await dbConnection.ExecuteAsync(deleteQuery);
                    return result > 0 ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
