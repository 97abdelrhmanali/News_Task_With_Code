using News.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications.newsSpec
{
    public class NewsSpecifications : BaseSpecifations<NewsEntity>
    {

        //this Constructor is used to retrieve All News with Categories and Source as Navigational property
        public NewsSpecifications(NewsSpecParams NewParam)
            : base()

        {
            Includes.Add(N => N.Categories);
            Includes.Add(N => N.Source);



            ApplyPagination((NewParam.pageIndex - 1) * NewParam.PageSize, NewParam.PageSize);
        }

        //this Constructor is used to retrieve specific News with Categories and Source as Navigational property
        public NewsSpecifications(int id): base(N => N.Id == id)
        {
            Includes.Add(N => N.Categories);
            Includes.Add(N => N.Source);
        }
    }
}
