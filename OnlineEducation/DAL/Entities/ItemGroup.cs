namespace OnlineEducation.DAL.Entities
{
    public class ItemGroup : IEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public int DependencyType { get; set; }

        public virtual Item Item { get; set; }
        public virtual Group Group { get; set; }
    }
}
