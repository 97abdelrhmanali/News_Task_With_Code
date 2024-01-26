using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications.newsSpec
{
    //this class is used to pass the pagination parameters
    public class NewsSpecParams
    {
        public int pageIndex { get; set; } = 1;
        const int maxSize = 10;
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxSize ? maxSize : value; }
        }

    }
}
