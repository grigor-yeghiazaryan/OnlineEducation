using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineEducation.DAL.Entities;
using OnlineEducation.DTO;

namespace OnlineEducation.BLL.Interfaces
{
    public interface IStudentService : IServiceBase<Student>
    {
        Task<StudentInfoModel> GetFullInfo(int id);
        Task<List<Item>> GetLessans(int id);
    }
}
