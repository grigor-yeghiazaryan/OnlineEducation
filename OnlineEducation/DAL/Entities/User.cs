namespace OnlineEducation.DAL.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public virtual Student Student { get; set; }
    }
}
