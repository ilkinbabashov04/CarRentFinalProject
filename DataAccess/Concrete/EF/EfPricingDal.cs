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
	public class EfPricingDal : BaseReporsitory<Pricing, BaseProjectContext>, IPricingDal
	{
		public EfPricingDal(BaseProjectContext context) : base(context)
		{
		}

        public List<GetPricingByCarIdDto> GetPricingByCarId(int carId)
        {
            using var context = new BaseProjectContext();

            var result = (from cp in context.CarPricings
                          join p in context.Pricings on cp.PricingId equals p.Id
                          where cp.CarId == carId && !cp.IsDelete
                          select new GetPricingByCarIdDto
                          {
                              CarId = cp.CarId,
                              PricingId = cp.PricingId,
                              Amount = cp.Amount
                          }).ToList();

            return result;
        }

    }
}
