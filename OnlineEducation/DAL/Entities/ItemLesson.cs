namespace OnlineEducation.DAL.Entities
{
    public class ItemLesson : IEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }

        public string GoogleDricveUrl { get; set; }
        public string VideoUrl { get; set; }
        public string QAUrl { get; set; }

        public virtual Item Item { get; set; }
    }
}
