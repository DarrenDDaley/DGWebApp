using DGWebApp.Models.Post;
using DGWebApp.Models.Put;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DGWebApp.Repository
{
    public interface ILandlordRepository
    {
        Task<int> Insert(PostLandlord landlord);
        Task<PutLandlord> Select(int id);
        Task<IEnumerable<PutLandlord>> EmailSelect(string email);
        Task<IEnumerable<PutLandlord>> PhoneSelect(string phone);
        Task<IEnumerable<PutProperty>> PropertySelect(int id);
        Task<bool> Update(int id, PutLandlord landlord);
        Task<bool> Delete(int id);
    }
}
