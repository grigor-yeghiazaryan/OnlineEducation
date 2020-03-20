using OnlineEducation.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.BLL.Interfaces
{
    public interface IStudentService : IServiceBase<Student>
    {
        Task<Student> Authenticate(string email, string password);
        Task<StudentInfoModel> GetStudentInfo(int id);
        Task<List<ItemModel>> GetLessans(int id);
    }
}