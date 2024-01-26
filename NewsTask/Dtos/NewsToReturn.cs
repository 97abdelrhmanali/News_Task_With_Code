using News.Core.Entities;

namespace NewsTask.Dtos
{
    public class NewsToReturn
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public DateTime PublishDate { get; set; }
        public ICollection<CategoryToReturn> Categories { get; set; } = new HashSet<CategoryToReturn>();
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceDescription { get; set; }
    }
}
