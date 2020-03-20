using OnlineEducation.DAL.Entities;

namespace OnlineEducation.DTO
{
    public class StudentInfoModel
    {
        public StudentInfoModel(Student student)
        {
            this.Id = student.Id;
            this.Name = student.Name;
            this.Surname = student.Surname;
            this.Patronymic = student.Patronymic;
            this.PhoneNumber = student.PhoneNumber;
            this.Email = student.Email;
            this.GroupId = student.GroupId;
            this.EducationalInstitutionType = student.EducationalInstitutionType;
            this.Course = student.Course;
            this.GroupName = student.Group?.GroupName;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }

        public int? EducationalInstitutionType { get; set; }
        public string Course { get; set; }

        public string GroupName { get; set; }
    }
}
