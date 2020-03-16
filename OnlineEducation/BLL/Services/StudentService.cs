using OnlineEducation.DAL;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.BLL.Services
{
    public class StudentService: ServiceBase<Student>, IStudentService
    {
        public StudentService(OnlineEducationDbContext dbContext) : base(dbContext) { }
    }
}
