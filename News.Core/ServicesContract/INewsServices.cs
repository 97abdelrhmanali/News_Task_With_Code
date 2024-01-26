using News.Core.Entities;
using News.Core.Specifications.newsSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.ServicesContract
{
    public interface INewsServices
    {
        Task<IReadOnlyList<NewsEntity>> GetAllNewsAsync(NewsSpecParams newsSpec);
        Task<NewsEntity?> GetNewsbyIdAsync(int id);
        Task<Category?> GetCategoryAsync(int id);
        Task<int> GetNewsCountAsync();
        Task<int> AddNewsAsync(NewsEntity news);
        Task<int> UpdateNewsAsync(NewsEntity news);
        Task<int> DeleteNews(NewsEntity news);

        Task<int> DeleteCategory(ICollection<Category> categories);
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<IReadOnlyList<Source>> GetSourceAsync();
    }
}
