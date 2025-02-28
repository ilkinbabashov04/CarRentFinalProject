using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Category : BaseEntity
	{
        public string Name { get; set; }
        //public List<Blog> Blogs { get; set; }
    }
}
