using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RentACar : BaseEntity
    {
        public int CarId { get; set; }
        public int LocationId { get; set; }
        public bool Available { get; set; }
        //public Location Location { get; set; }
        //public Car Car { get; set; }
    }
}
