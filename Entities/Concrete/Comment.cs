using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int BlogId { get; set; }
        //public Blog Blog { get; set; }
    }
}
