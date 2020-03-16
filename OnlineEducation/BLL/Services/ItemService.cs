using OnlineEducation.DAL;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.BLL.Services
{
    public class ItemService : ServiceBase<Item>, IItemService
    {
        public ItemService(OnlineEducationDbContext dbContext) : base(dbContext) { }
    }
}
