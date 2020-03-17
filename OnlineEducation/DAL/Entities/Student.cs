namespace OnlineEducation.DAL.Entities
{
    public class Student : User, IEntity
    {
        public int GroupId { get; set; }

        public int EducationalInstitutionType { get; set; }
        public int Course { get; set; }

        public virtual Group Group { get; set; }
    }
}
