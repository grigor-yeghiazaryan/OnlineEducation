using OnlineEducation.DAL;
using OnlineEducation.BLL.Interfaces;
using OnlineEducation.DAL.Entities;

namespace OnlineEducation.BLL.Services
{
    public class ItemLessonService : ServiceBase<ItemLesson>, IItemLessonService
    {
        public ItemLessonService(OnlineEducationDbContext dbContext) : base(dbContext) { }
    }
}
