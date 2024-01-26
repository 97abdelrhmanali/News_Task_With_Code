using News.Core.Entities;

namespace NewsTask.Dtos
{
    public class NewsToUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public ICollection<int>? Categories { get; set; } = new HashSet<int>();
        public int? SourceId { get; set; }
        //public Source? Source { get; set; }
    }
}
