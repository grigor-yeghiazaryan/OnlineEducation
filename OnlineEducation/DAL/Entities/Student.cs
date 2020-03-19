namespace OnlineEducation.DAL.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int GroupId { get; set; }

        public int? EducationalInstitutionType { get; set; }
        public string Course { get; set; }

        public virtual Group Group { get; set; }
    }
}
