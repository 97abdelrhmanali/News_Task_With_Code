using Microsoft.EntityFrameworkCore;
using News.Core.Entities;
using News.Core.Specifications;
using News.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Repository
{
    //used to Generate dynamic Query
    internal static class SpecificationEvaluation<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> context , ISpecifications<T> spec)
        {
            var query = context;

            if(spec.Predicate  != null)
                query = query.Where(spec.Predicate);

            if(spec.isPaginationed == true)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return query;
        }
    }
}
