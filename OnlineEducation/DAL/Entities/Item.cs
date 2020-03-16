using System.Collections.Generic;

namespace OnlineEducation.DAL.Entities
{
    public class Item : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int EducationalInstitutionType { get; set; }
        public int Course { get; set; }
        public bool IsExam { get; set; }

        public virtual ICollection<ItemsLesson> ItemsLessons { get; set; }
        public virtual ICollection<ItemGroup> ItemGroups { get; set; }
    }
}