using Dapper;
using DGWebApp.Models.Post;
using DGWebApp.Models.Put;
using DGWebApp.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DGWebApp.Repository
{
    public class LandlordRepository : ILandlordRepository
    {
        private readonly IDbConnection db;
        private readonly IOptions<ConnectionStrings> connectionStrings;

        public LandlordRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            this.connectionStrings = connectionStrings ?? throw new ArgumentNullException(nameof(connectionStrings));

            db = new SqlConnection(this.connectionStrings.Value.Default);
        }

        public async Task<int> Insert(PostLandlord landlord)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string insertQuery = "INSERT INTO dbo.Landlords (Forename, Surname, Phone, Email)" +
                                         "VALUES(@Forename, @Surname, @Phone, @Email);" +
                                         "SELECT CAST(SCOPE_IDENTITY() AS int)";

                    dbConnection.Open();
                    var idCollection = await dbConnection.QueryAsync<int>(insertQuery, landlord);
                    return idCollection.FirstOrDefault();
                }
            }
            catch
            {
                return 0;
            }
        }

        public async Task<PutLandlord> Select(int id)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string selectQuery = $"SELECT * FROM dbo.Landlords WHERE LandlordId = { id }";

                    dbConnection.Open();
                    var landlord = await  dbConnection.QueryAsync<PutLandlord>(selectQuery);
                    return landlord.First();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PutLandlord>> EmailSelect(string email)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string selectQuery = $"SELECT * FROM dbo.Landlords WHERE Email = @Email";

                    dbConnection.Open();
                    var landlords = await dbConnection.QueryAsync<PutLandlord>(selectQuery, new { Email = email });
                    return landlords;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PutLandlord>> PhoneSelect(string phone)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string selectQuery = $"SELECT * FROM dbo.Landlords WHERE Phone = @Phone";

                    dbConnection.Open();
                    var landlords = await dbConnection.QueryAsync<PutLandlord>(selectQuery, new { Phone = phone });
                    return landlords;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<PutProperty>> PropertySelect(int id)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string selectQuery = $"SELECT * FROM dbo.Properties WHERE LandlordId = { id }";

                    dbConnection.Open();
                    var properties = await dbConnection.QueryAsync<PutProperty>(selectQuery);
                    return properties;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Update(int id, PutLandlord landlord)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    var result = 0;
                    var updateFields = UpdateStringConstruction(landlord);

                    dbConnection.Open();

                    foreach (var updateField in updateFields)
                    {

                        string updateQuery = $"UPDATE dbo.Landlords " +
                                             $"SET { updateField }" +
                                             $"WHERE LandlordId = { id }";

       
                        result = await dbConnection.ExecuteAsync(updateQuery, landlord);
                    }
                    return result > 0 ? true : false;
                }
            }
            catch( Exception ex)
            {
                return false;
            }
        }

        private IEnumerable<string> UpdateStringConstruction(PutLandlord landlord)
        {
            List<string> fields = new List<string>();

            if(!string.IsNullOrWhiteSpace(landlord.Forename)) {
                fields.Add("Forename = @Forename ");
            }

            if (!string.IsNullOrWhiteSpace(landlord.Surname)) {
                fields.Add("Surname = @Surname ");
            }

            if (!string.IsNullOrWhiteSpace(landlord.Email)) {
                fields.Add("Email = @Email ");
            }

            if (!string.IsNullOrWhiteSpace(landlord.Phone)) {
                fields.Add("Phone = @Phone ");
            }

            return fields.ToArray();
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = db)
                {
                    string deleteQuery = $"DELETE FROM dbo.Landlords WHERE LandlordId = { id }";

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
