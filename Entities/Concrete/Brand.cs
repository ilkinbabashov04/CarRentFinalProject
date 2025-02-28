using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Brand : BaseEntity
	{
        public string Name { get; set; }
        //public int CarId { get; set; }
        //public List<Car> Cars { get; set; }
    }
}
