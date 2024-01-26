using Microsoft.EntityFrameworkCore;
using News.Core;
using News.Core.ServicesContract;
using News.Repository;
using News.Repository.Data;
using News.Services;
using NewsTask.helper;

namespace NewsTask
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region DI and Configration Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<NewsDbContext>(option =>
            {
                option.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWorks));
            builder.Services.AddScoped(typeof(INewsServices), typeof(NewsService));
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            #endregion

            #region MiddleWares and update data

            var app = builder.Build();



            #region Update DataBase

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<NewsDbContext>();
            var Logger = services.GetRequiredService<ILoggerFactory>();

            try 
            {
                await dbContext.Database.MigrateAsync();
                await ContextSeeding.SeedDataAsync(dbContext, Logger);
            }
            catch (Exception ex)
            {
                var log = Logger.CreateLogger<Program>();
                log.LogError(ex, "Error happens while updating database");
            }



            #endregion



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
