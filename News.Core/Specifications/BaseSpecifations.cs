using News.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
    //this class is used as abstract class for all Specification classes 
    public class BaseSpecifations<TEntity> : ISpecifications<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool isPaginationed { get; set; }
        public BaseSpecifations(){}

        public BaseSpecifations(Expression<Func<TEntity,bool>> predicate)
        {
            Predicate = predicate;
        }


        public void ApplyPagination(int skip , int take)
        {
            isPaginationed = true;
            Skip = skip;
            Take = take;
        }
    }
}
