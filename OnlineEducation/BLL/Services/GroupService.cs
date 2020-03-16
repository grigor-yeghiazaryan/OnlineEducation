using OnlineEducation.DAL;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.BLL.Services
{
    public class GroupService : ServiceBase<Group>, IGroupService
    {
        public GroupService(OnlineEducationDbContext dbContext) : base(dbContext) { }
    }
}
