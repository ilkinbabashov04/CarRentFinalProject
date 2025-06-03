using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Location : BaseEntity
	{
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public List<RentACar> RentACars { get; set; }
    }
}
