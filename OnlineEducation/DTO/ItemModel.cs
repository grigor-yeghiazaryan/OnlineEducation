using System.Collections.Generic;

namespace OnlineEducation.DAL.Entities
{
    public class ItemModel
    {
        public ItemModel(Item item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.EducationalInstitutionType = item.EducationalInstitutionType;
            this.Course = item.Course;
            this.IsExam = item.IsExam;
            this.ItemsLessons = new List<ItemLesson>();

            foreach (var itemLesson in item.ItemsLessons)
            {
                itemLesson.Item = null;
                ItemsLessons.Add(itemLesson);
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int EducationalInstitutionType { get; set; }
        public int Course { get; set; }
        public bool IsExam { get; set; }
        public int DependencyType { get; set; }
        public List<ItemLesson> ItemsLessons { get; set; }
    }
}