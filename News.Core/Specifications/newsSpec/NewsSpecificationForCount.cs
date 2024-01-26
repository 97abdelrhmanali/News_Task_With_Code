using News.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications.newsSpec
{
    //this specification is used to retrieve the count of all news due to pagination
    public class NewsSpecificationForCount : BaseSpecifations<NewsEntity>
    {
        public NewsSpecificationForCount()
        : base()

        {
            Includes.Add(N => N.Categories);
            Includes.Add(N => N.Source);
        }
    }
 }
