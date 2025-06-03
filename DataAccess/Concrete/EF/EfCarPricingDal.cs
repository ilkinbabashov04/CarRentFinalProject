using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfCarPricingDal : BaseReporsitory<CarPricing, BaseProjectContext>, ICarPricingDal
	{
		public EfCarPricingDal(BaseProjectContext context) : base(context)
		{
		}

        public List<CarPricingDto> GetAll()
        {
            var context = new BaseProjectContext();

            var result = from p in context.CarPricings
                         where p.IsDelete == false
                         where p.PricingId == 1
                         join c in context.Cars on p.CarId equals c.Id
                         where c.IsDelete == false
                         join b in context.Brands on c.BrandId equals b.Id
                         select new CarPricingDto
                         {
                             CarId = p.CarId,
                             CarPricingId = p.Id,
                             Amount = p.Amount,
                             BrandName = b.Name,
                             ModelName = c.Model,
                             CoverImageUrl = c.CoverImageUrl,

                             Reservations = context.Reservations
                                .Where(r => r.CarId == c.Id && r.DropOffDate >= DateTime.Today)
                                .Select(r => new ReservationDto
                                {
                                    PickUpDate = r.PickUpDate,
                                    PickUpTime = r.PickUpTime,
                                    DropOffDate = r.DropOffDate,
                                    DropOffTime = r.DropOffTime
                                }).ToList()
                         };

            return result.ToList();
        }

        public List<CarPricingDto> GetCarPricingByCarId(int id)
        {
            using var context = new BaseProjectContext();

            var result = context.CarPricings
                .Where(cp => cp.CarId == id && cp.IsDelete == false)
                .Select(cp => new CarPricingDto
                {
                    CarPricingId = cp.Id,
                    CarId = cp.CarId,
                    PricingId = cp.PricingId,
                    Amount = cp.Amount,
                    IsDelete = cp.IsDelete
                })
                .ToList();

            return result;
        }


        public List<GetCarPricingWithTimePeriodDto> GetCarPricingWithTimePeriods()
		{
			using var context = new BaseProjectContext();

			var result = from cp in context.CarPricings
						 where cp.IsDelete == false
						 join p in context.Pricings
						 on cp.PricingId equals p.Id
						 join c in context.Cars
						 on cp.CarId equals c.Id
						 where c.IsDelete == false
						 join b in context.Brands
						 on c.BrandId equals b.Id
						 group cp by new { c.Id, c.Model, c.CoverImageUrl, cp.CarId, Brand = b.Name } into g
						 select new GetCarPricingWithTimePeriodDto
						 {
                             CarId = g.Key.CarId,
							 Brand = g.Key.Brand,
							 Model = g.Key.Model,
							 CoverImageUrl = g.Key.CoverImageUrl,
							 DailyAmount = g.Where(x => x.PricingId == 1).Sum(x => x.Amount),
							 WeeklyAmount = g.Where(x => x.PricingId == 2).Sum(x => x.Amount),
							 MonthlyAmount = g.Where(x => x.PricingId == 4).Sum(x => x.Amount)
						 };

			return result.ToList();

		}

        

	}
}
