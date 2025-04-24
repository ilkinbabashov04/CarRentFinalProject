using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarPricingManager(ICarPricingDal carPricingDal) : ICarPricingService
    {
        private readonly ICarPricingDal _carPricingDal = carPricingDal;
        public IDataResult<List<CarPricingDto>> GetAll()
        {
            var result = _carPricingDal.GetAll();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarPricingDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<CarPricingDto>>(result, "Not found!");
            }
        }

		public IDataResult<List<GetCarPricingWithTimePeriodDto>> GetCarPricingWithTimePeriods()
		{
			var result = _carPricingDal.GetCarPricingWithTimePeriods();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<GetCarPricingWithTimePeriodDto>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<GetCarPricingWithTimePeriodDto>>(result, "Not found!");
			}
		}
	}
}
