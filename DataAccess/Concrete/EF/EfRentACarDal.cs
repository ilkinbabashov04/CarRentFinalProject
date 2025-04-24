using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfRentACarDal : BaseReporsitory<RentACar, BaseProjectContext>, IRentACarDal
    {
        public EfRentACarDal(BaseProjectContext context) : base(context)
        {

        }

        public List<BarGraphDto> GetBarGraphDetail()
        {
            var context = new BaseProjectContext();

            var result = (from location in context.Locations
                          where location.IsDelete == false
                          join car in context.RentACars
                          on location.Id equals car.LocationId
                          where car.Available == true && car.IsDelete == false
                          group car by new { location.Id, location.Name } into g
                          select new BarGraphDto
                          {
                              LocationName = g.Key.Name,
                              AvailableCarsCount = g.Count()
                          }).ToList();

            return result;
        }


        public List<CarGetByFilterDto> GetByFilter(int locationId, bool available)
        {
            using (var context = new BaseProjectContext())
            {
                var result = from r in context.RentACars
                             where r.LocationId == locationId
                             where !r.IsDelete
                             where r.Available == available
                             join c in context.Cars on r.CarId equals c.Id
                             join b in context.Brands on c.BrandId equals b.Id
                             join cp in context.CarPricings on c.Id equals cp.CarId into pricingGroup
                             from cp in pricingGroup.DefaultIfEmpty()
                             select new CarGetByFilterDto
                             {
                                 CarId = r.CarId,
                                 Brand = b.Name,
                                 Model = c.Model,
                                 Amount = cp != null ? cp.Amount : 0,
                                 CoverImageUrl = c.CoverImageUrl,
                             };

                return result.ToList();
            }
        }

    }
}
