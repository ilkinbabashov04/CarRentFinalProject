using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class CarDescription : BaseEntity
	{
        public int CarId { get; set; }
        //public Car Car { get; set; }
        public string Detail { get; set; }
    }
}
