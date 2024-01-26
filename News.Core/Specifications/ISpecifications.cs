using News.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications
{
    public interface ISpecifications<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public List<Expression<Func<TEntity,object>>> Includes { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool isPaginationed { get; set; }

    }
}
