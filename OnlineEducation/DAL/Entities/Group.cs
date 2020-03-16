using System.Collections.Generic;

namespace OnlineEducation.DAL.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ItemGroup> ItemGroups { get; set; }
    }
}