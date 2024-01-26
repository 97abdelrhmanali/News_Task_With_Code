using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Core;
using News.Core.Entities;
using News.Core.ServicesContract;
using News.Core.Specifications.newsSpec;
using News.Repository.Repositories;
using News.Services;
using NewsTask.Dtos;

namespace NewsTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsServices _newsServices;
        private readonly IMapper _mapper;

        public NewsController(INewsServices newsServices, IMapper mapper)
        {
            _newsServices = newsServices;
            _mapper = mapper;
        }


        //GetAll News + Applying Pagination with max PageSize = 5 
        //https://localhost:7091/api/News?pageSize=10&PageIndex=2  Get
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<NewsToReturn>>> GetAllAsync([FromQuery]NewsSpecParams newsSpec)
        {
            var News = await _newsServices.GetAllNewsAsync(newsSpec);
            var count = await _newsServices.GetNewsCountAsync();
            var NewsToReturn = _mapper.Map<IReadOnlyList<NewsToReturn>>(News);
            return Ok(new 
            {
                PageSize = newsSpec.PageSize,
                PageIndex = newsSpec.pageIndex,
                Count = count,
                Data = NewsToReturn
            });
        }

        //used to get Specific News by id
        //https://localhost:7091/api/News/{id}  Get
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsToReturn>> GetNews(int id)
        {
            var news = await _newsServices.GetNewsbyIdAsync(id);
            if(news == null) return NotFound();
            var NewsToReturn = _mapper.Map<NewsToReturn>(news);
            return Ok(NewsToReturn);
        }

        //used for adding news in database
        //https://localhost:7091/api/News  post
        [HttpPost]
        public async Task<ActionResult<string>> AddNews(NewsEntity newsEntity)
        {
            var count = await _newsServices.AddNewsAsync(newsEntity);
            if (count > 0) return Ok("Successfully Added");
            else return BadRequest();
        }

        //used for Update news in database
        //https://localhost:7091/api/News  put
        [HttpPut]
        public async Task<ActionResult<string>> UpdatedNews(NewsToUpdate newsEntity)
        {
            //Get old News before updated
            var news = await _newsServices.GetNewsbyIdAsync(newsEntity.Id);
            if (news is null) return NotFound();

            if (news.Categories is not null)
            {
                //Checking if the Categories exists or not before deleting the relationship
                foreach (var item in newsEntity.Categories)
                {
                    var cat = await _newsServices.GetCategoryAsync(item);
                    if (cat is null)
                        return NotFound();
                }
                //delete all the old relationships in NewsCategory Table
                await _newsServices.DeleteCategory(news.Categories);

                //Adding the new relationships in NewsCategory Table
                foreach (var item in newsEntity.Categories)
                {
                    var cat = await _newsServices.GetCategoryAsync(item);
                    if (cat is not null)
                        news.Categories.Add(cat);
                }
            }
            
            // add the new data to the entity to update
            #region Manual Mapping
            news.Title = newsEntity.Title;
            news.Content = newsEntity.Content;
            news.PublishDate = newsEntity.PublishDate;
            news.Photo = newsEntity.Photo;

            if (newsEntity.SourceId.HasValue)
                news.SourceId = newsEntity.SourceId.Value; 
            #endregion

            //update the News in the database
            var count = await _newsServices.UpdateNewsAsync(news);
            if (count > 0) return Ok("Successfully Updated");
            else return BadRequest();
        }

        //Delete specific News Entity
        //https://localhost:7091/api/News/{id}  Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteNews(int id)
        {
            var news = await _newsServices.GetNewsbyIdAsync(id);
            if (news == null) return NotFound();
            var count = await _newsServices.DeleteNews(news);
            if (count > 0) return Ok("Successfully Deleted");
            else return BadRequest();
        }

        //Retrieve All Categories
        //https://localhost:7091/api/News/Category  Get
        [HttpGet("Category")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategoriesAsync()
        {
            var category = await _newsServices.GetCategoriesAsync();
            var categoryToReturn = _mapper.Map<IReadOnlyList<CategoryToReturn>>(category);
            return Ok(categoryToReturn);
        }

        //Retrieve All Sources
        //https://localhost:7091/api/News/Source  Get
        [HttpGet("Source")]
        public async Task<ActionResult<IReadOnlyList<Source>>> GetAllSourcesAsync()
                => Ok(await _newsServices.GetSourceAsync());

    }
}
