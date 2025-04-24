using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class CarFeature : BaseEntity
	{
        public int CarId { get; set; }
        //public Car Car { get; set; }
        public int FeatureId { get; set; }
        //public Feature Feature { get; set; }
        public bool Available { get; set; }
    }
}
