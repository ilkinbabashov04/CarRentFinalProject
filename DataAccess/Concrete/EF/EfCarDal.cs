using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EF
{
    public class EfCarDal : BaseReporsitory<Car, BaseProjectContext>, ICarDal
    {
        public EfCarDal(BaseProjectContext context) : base(context)
        {
        }

        public List<CarDto> AddCar(CarDto car)
        {
            var context = new BaseProjectContext();
            var result = from c in context.Cars
                         where c.IsDelete == false
                         join b in context.Brands
                         on c.BrandId equals b.Id
                         select new CarDto
                         {
                             BrandId = b.Id,
                             Model = c.Model,
                             CoverImageUrl = c.CoverImageUrl,
                             BigImageUrl = c.BigImageUrl,
                             Km = c.Km,
                             Transmission = c.Transmission,
                             Seat = c.Seat,
                             Luggage = c.Luggage,
                             Fuel = c.Fuel,
                         };
            return result.ToList();
        }

        public List<CarDto> GetAvailableCars(int locationId, DateTime pickupDateTime, DateTime dropoffDateTime)
        {
            using var context = new BaseProjectContext();

            // Burada əvvəlcə həmin lokasiyada və available olan RentACar-ları tapırıq
            var availableCarIds = context.RentACars
                .Where(r => r.LocationId == locationId && r.Available == true)
                .Select(r => r.CarId)
                .Distinct()
                .ToList();

            // Daha sonra bu maşınların reservationları yoxlayırıq ki, həmin tarix aralığında bağlı olmasın
            var reservedCarIds = context.Reservations
    .Where(res =>
        (pickupDateTime >= res.PickUpDate && pickupDateTime < res.DropOffDate) ||
        (dropoffDateTime > res.PickUpDate && dropoffDateTime <= res.DropOffDate) ||
        (pickupDateTime <= res.PickUpDate && dropoffDateTime >= res.DropOffDate)
    )
    .Select(res => res.CarId)
    .Distinct()
    .ToList();


            // Sonra available və reservasyonda olmayan maşınları seçirik
            var result = from c in context.Cars
                         join b in context.Brands on c.BrandId equals b.Id
                         join cp in context.CarPricings on c.Id equals cp.CarId
                         where availableCarIds.Contains(c.Id)
                               && !reservedCarIds.Contains(c.Id)
                               && !c.IsDelete
                               && cp.PricingId == 1
                         select new CarDto
                         {
                             id = c.Id,
                             BrandId = b.Id,
                             BrandName = b.Name,
                             Model = c.Model,
                             CoverImageUrl = c.CoverImageUrl,
                             BigImageUrl = c.BigImageUrl,
                             Km = c.Km,
                             Transmission = c.Transmission,
                             Seat = c.Seat,
                             Luggage = c.Luggage,
                             Fuel = c.Fuel,
                             Amount = cp.Amount
                         };

            return result.ToList();
        }


        public List<CarDto> GetCarWithBrandName()
        {
            var context = new BaseProjectContext();
            var result = from c in context.Cars
                         where c.IsDelete == false
                         join b in context.Brands
                         on c.BrandId equals b.Id
                         select new CarDto
                         {
                             id = c.Id,
                             BrandId = b.Id,
                             BrandName = b.Name,
                             Model = c.Model,
                             CoverImageUrl = c.CoverImageUrl,
                             BigImageUrl = c.BigImageUrl,
                             Km = c.Km,
                             Transmission = c.Transmission,
                             Seat = c.Seat,
                             Luggage = c.Luggage,
                             Fuel = c.Fuel,
                         };
            return result.ToList();
        }

        public List<CarDto> GetLastFiveCarsWithBrand()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars
                    .Where(c => !c.IsDelete)
                    .Join(context.CarPricings,
                        car => car.Id,
                        cp => cp.CarId,
                        (car, cp) => new { car, cp })
                    .Where(x => x.cp.PricingId == 1 && x.cp.IsDelete == false)
                    .Join(context.Brands,
                        x => x.car.BrandId,
                        brand => brand.Id,
                        (x, brand) => new CarDto
                        {
                            id = x.car.Id,
                            BrandId = brand.Id,
                            BrandName = brand.Name,
                            Model = x.car.Model,
                            CoverImageUrl = x.car.CoverImageUrl,
                            BigImageUrl = x.car.BigImageUrl,
                            Km = x.car.Km,
                            Transmission = x.car.Transmission,
                            Seat = x.car.Seat,
                            Luggage = x.car.Luggage,
                            Fuel = x.car.Fuel,
                            Amount = x.cp.Amount
                        })
                    .OrderByDescending(c => c.id)
                    .ToList() // Execute SQL query here
                    .DistinctBy(c => c.id) // Now safe to use client-side
                    .Take(5) // Get last 5 distinct
                    .ToList();

                return result;
            }
        }


        public List<PieChartDto> GetPieChartDetail()
        {
            using var context = new BaseProjectContext();

            var result = (from car in context.Cars
                          where car.IsDelete == false
                          join brand in context.Brands on car.BrandId equals brand.Id
                          where brand.IsDelete == false
                          group car by brand.Name into g
                          select new PieChartDto
                          {
                              BrandName = g.Key,
                              Count = g.Count()
                          })
                          .ToList();

            return result;
        }
       
    }
}
