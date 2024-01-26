using News.Core;
using News.Core.Entities;
using News.Core.RepoInterfaces;
using News.Repository.Data;
using News.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
    public class UnitOfWorks : IUnitOfWork, IAsyncDisposable
    {
        private readonly NewsDbContext _dbContext;
        private Hashtable repository;

        public UnitOfWorks(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
            repository = new Hashtable();
        }


        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name;

            if (!repository.ContainsKey(Key))
            {
                var repo = new GenericRepository<TEntity>(_dbContext);
                repository.Add(Key, repo);
            }

            return repository[Key] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
