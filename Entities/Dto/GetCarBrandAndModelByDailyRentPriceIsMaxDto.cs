using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class GetCarBrandAndModelByDailyRentPriceIsMaxDto : IDto
    {
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public decimal Amount { get; set; }
    }
}
