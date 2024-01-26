using News.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Specifications.CategorySpec
{
    //this specification is used to retrieve specific category with news as Navigational property
    public class CategorySpecification : BaseSpecifations<Category>
    {
        public CategorySpecification(int id):base(C => C.Id == id)
        {
            Includes.Add(C => C.News);
        }
    }
}
