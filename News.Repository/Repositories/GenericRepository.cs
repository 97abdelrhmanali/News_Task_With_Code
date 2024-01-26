using Microsoft.EntityFrameworkCore;
using News.Core.Entities;
using News.Core.RepoInterfaces;
using News.Core.Specifications;
using News.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly NewsDbContext _dbContext;

        public GenericRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Appling SpecificationEvaluation class to generate dynamic query
        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluation<T>.GetQuery(_dbContext.Set<T>(), spec);
        }


        //used to retrieve all data of entity with it's navigational property
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        //used to retrieve Specific data of entity with it's navigational property
        public async Task<T?> GetItemWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        ////used to retrieve all data of entity without it's navigational property
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        //used to retrieve Specific data of entity without it's navigational property
        public async Task<T?> GetItemByIdAsync(int id)
                => await _dbContext.Set<T>().FindAsync(id);

        //used to Add entity 
        public async Task AddAsync(T Entity)
              => await _dbContext.AddAsync(Entity);

        //used to Delete entity
        public void Delete(T Entity)
             =>  _dbContext.Remove(Entity);

        //used to Update entity
        public void Update(T Entity)
            => _dbContext.Update(Entity);

        public async Task<int> GetCountWithSpecAsync(ISpecifications<T> spec)
                => await ApplySpecification(spec).CountAsync();
    }
}
