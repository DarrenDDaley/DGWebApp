using DGWebApp.Models.Post;
using DGWebApp.Models.Put;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DGWebApp.Repository
{
    public interface IProperyRepositroy
    {
        Task<int> Insert(PostProperty property);
        Task<PutProperty> Select(int id);
        Task<IEnumerable<PutProperty>> SelectAddress(string address);
        Task<bool> Update(int id, PutProperty property);
        Task<bool> Delete(int id);
    }
}
