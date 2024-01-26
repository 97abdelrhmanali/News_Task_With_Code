using Microsoft.Extensions.Logging;
using News.Core.Entities;
using News.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace News.Repository
{
    public  class ContextSeeding
    {
        public static async Task SeedDataAsync(NewsDbContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if(context?.Categories.Count() == 0)
                {
                    var categoryData = File.ReadAllText("../News.Repository/Data/DataSeed/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoryData);
                    if(categories?.Count() > 0 ) 
                        foreach (var category in categories)
                            await context.Categories.AddAsync(category);

                    await context.SaveChangesAsync();
                }


                if (context?.Sources.Count() == 0)
                {
                    var sourceData = File.ReadAllText("../News.Repository/Data/DataSeed/source.json");
                    var sources = JsonSerializer.Deserialize<List<Source>>(sourceData);
                    if (sources?.Count() > 0)
                        foreach (var source in sources)
                            await context.Sources.AddAsync(source);

                    await context.SaveChangesAsync();
                }


                if (context?.News.Count() == 0)
                {
                    var newsData = File.ReadAllText("../News.Repository/Data/DataSeed/news.json");
                    var News = JsonSerializer.Deserialize<List<NewsEntity>>(newsData);
                    if (News?.Count() > 0)
                        foreach (var item in News)
                            await context.News.AddAsync(item);

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex) 
            {
                var Logger = loggerFactory.CreateLogger<ContextSeeding>();
                Logger.LogError(ex, "error happens while Adding Data");
            }
        }
    }
}
