using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class GetCarPricingWithTimePeriodDto : IDto
	{
        public string Brand { get; set; }
        public string Model { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal DailyAmount { get; set; }
		public decimal WeeklyAmount { get; set; }
		public decimal MonthlyAmount { get; set; }
	}
}
