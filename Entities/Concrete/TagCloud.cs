using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TagCloud : BaseEntity
    {
        public string Title { get; set; }
        public int BlogId { get; set; }
        //public Blog Blog { get; set; }
    }
}
