using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Core.Entities
{
    public class Source : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
