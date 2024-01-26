using Microsoft.EntityFrameworkCore;
using News.Core;
using News.Core.Entities;
using News.Core.ServicesContract;
using News.Core.Specifications.CategorySpec;
using News.Core.Specifications.newsSpec;
using News.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Services
{
    public class NewsService : INewsServices
    {
        private readonly IUnitOfWork _unitOfWorks;

        public NewsService(IUnitOfWork unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }

        //used to get all news with it's Categories and Source
        public async Task<IReadOnlyList<NewsEntity>> GetAllNewsAsync(NewsSpecParams newsSpec)
            =>  await _unitOfWorks.Repository<NewsEntity>()
                                  .GetAllWithSpecAsync(new NewsSpecifications(newsSpec));

        //used to get Specific news with it's Categories and Source
        public async Task<NewsEntity?> GetNewsbyIdAsync(int id)
        {
            var news = await _unitOfWorks.Repository<NewsEntity>().
                                         GetItemWithSpecAsync(new NewsSpecifications(id));
            if (news == null) return null;
            return news;
        }

        //Get Specific Category by id
        public async Task<Category?> GetCategoryAsync(int id)
        {
            var category = await _unitOfWorks.Repository<Category>().
                                         GetItemWithSpecAsync(new CategorySpecification(id));
            if (category == null) return null;
            return category;
        }

        //Get All Categories with out its navigation property
        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
            => await _unitOfWorks.Repository<Category>().GetAllAsync();

        //Get the Totall counts of News
        public async Task<int> GetNewsCountAsync()
             => await _unitOfWorks.Repository<NewsEntity>()
                                  .GetCountWithSpecAsync(new NewsSpecificationForCount());
        
        //Get All Sources
        public async Task<IReadOnlyList<Source>> GetSourceAsync()
                => await _unitOfWorks.Repository<Source>().GetAllAsync();

        //used to add News in database
        public async Task<int> AddNewsAsync(NewsEntity news)
        {
            ICollection<Category> cat = new List<Category>();
            foreach (var item in news.Categories)
            {
                var cate = await _unitOfWorks.Repository<Category>().GetItemByIdAsync(item.Id);
                cat.Add(cate);
            }

            var source = await _unitOfWorks.Repository<Source>().GetItemByIdAsync(news.SourceId);
            var newsToAdd = new NewsEntity(news.Title, news.Content, news.Photo, DateTime.Now, cat, news.SourceId, source);

            await _unitOfWorks.Repository<NewsEntity>().AddAsync(newsToAdd);
            return await _unitOfWorks.CompleteAsync();
        }

        //used to update News in database
        public async Task<int> UpdateNewsAsync(NewsEntity news)
        { 
            _unitOfWorks.Repository<NewsEntity>().Update(news);
            return await _unitOfWorks.CompleteAsync();
        }

        //used to Delete News From database
        public async Task<int> DeleteNews(NewsEntity news)
        {
            _unitOfWorks.Repository<NewsEntity>().Delete(news);
            return await _unitOfWorks.CompleteAsync();
        }

        //used To Delete Categories for Specific News
        public async Task<int> DeleteCategory(ICollection<Category> categories)
        {
            categories.Clear();
            return await _unitOfWorks.CompleteAsync();
        }

    }
}
