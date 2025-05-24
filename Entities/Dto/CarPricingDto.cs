using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CarPricingDto : IDto
    {
        public int CarPricingId { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public decimal Amount { get; set; }
        public string CoverImageUrl { get; set; }
        public List<ReservationDto> Reservations { get; set; } = new();
    }
}
