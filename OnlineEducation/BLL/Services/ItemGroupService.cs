using OnlineEducation.DAL;
using OnlineEducation.DAL.Entities;
using OnlineEducation.BLL.Interfaces;

namespace OnlineEducation.BLL.Services
{
    public class ItemGroupService : ServiceBase<ItemGroup>, IItemGroupService
    {
        public ItemGroupService(OnlineEducationDbContext dbContext) : base(dbContext) { }
    }
}
