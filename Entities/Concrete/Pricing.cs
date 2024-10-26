using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Pricing : BaseEntity
	{
        public string Name { get; set; }
        //public List<CarPricing> CarPricings { get; set; }
    }
}
