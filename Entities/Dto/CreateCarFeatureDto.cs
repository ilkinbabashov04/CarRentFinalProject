using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CreateCarFeatureDto : IDto
    {
        public int CarId { get; set; }
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }
        public bool Available { get; set; }
    }
}
