using Core.DataAccess.Concrete;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public List<CarDto> GetCarWithBrandName()
		{
			var context = new BaseProjectContext();
			var result = from c in context.Cars
						 where c.IsDelete == false
						 join b in context.Brands
						 on c.BrandId equals b.Id
						 select new CarDto
						 {
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
			var context = new BaseProjectContext();
			var values = from c in context.Cars
                         where c.IsDelete == false
                         join b in context.Brands
                         on c.BrandId equals b.Id
                         select new CarDto
                         {
							 CarId = c.Id,
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
            return values.Take(5).ToList();
        }
    }
}
