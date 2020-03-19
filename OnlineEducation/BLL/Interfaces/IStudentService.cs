using OnlineEducation.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineEducation.BLL.Interfaces
{
    public interface IStudentService : IServiceBase<Student>
    {
        Task<Student> Authenticate(string email, string password);
        Task<List<ItemModel>> GetLessans(int id);
    }
}