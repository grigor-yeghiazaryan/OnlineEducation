namespace OnlineEducation.DAL.Entities
{
    public class StudentModel : Student
    {
        public StudentModel() { }

        public StudentModel(User user, Student student)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Patronymic = user.Patronymic;
            this.PhoneNumber = user.PhoneNumber;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Token = user.Token;
            this.GroupId = student.GroupId;
            this.EducationalInstitutionType = student.EducationalInstitutionType;
            this.Course = student.Course;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public User GetUser()
        {
            var user = new User
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                Patronymic = this.Patronymic,
                PhoneNumber = this.PhoneNumber,
                Email = this.Email,
                Password = this.Password,
                Token = this.Token,
            };
            return user;
        }
    }
}
