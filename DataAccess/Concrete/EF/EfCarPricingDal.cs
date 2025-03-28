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
                         where p.PricingId == 2
                         join c in context.Cars
                         on p.CarId equals c.Id
                         join b in context.Brands
                         on c.BrandId equals b.Id
                         select new CarPricingDto
                         {
                             CarId = p.CarId,
                             CarPricingId = p.Id,
                             Amount = p.Amount,
                             BrandName = b.Name,
                             ModelName = c.Model,
                             CoverImageUrl = c.CoverImageUrl,
                         };

            return result.ToList();

        }
    }
}
