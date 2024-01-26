using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Entities
{
    public class NewsEntity : BaseEntity
    {
        public NewsEntity()
        {
                
        }
        public NewsEntity(string title, string content, string photo, DateTime publishDate, ICollection<Category> categories, int sourceId, Source source)
        {
            Title = title;
            Content = content;
            Photo = photo;
            PublishDate = publishDate;
            Categories = categories;
            SourceId = sourceId;
            Source = source;
        }

        public NewsEntity(int id,string title, string content, string photo, DateTime publishDate, ICollection<Category> categories, int sourceId, Source source)
        {
            Id = id;
            Title = title;
            Content = content;
            Photo = photo;
            PublishDate = publishDate;
            Categories = categories;
            SourceId = sourceId;
            Source = source;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Photo { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public int SourceId { get; set; }
        public virtual Source Source { get; set; } 
    }
}
