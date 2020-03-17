namespace OnlineEducation.DAL.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public int EducationalInstitutionType { get; set; }
        public int Course { get; set; }

        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}
