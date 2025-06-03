using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
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

        public IResult Add(CarPricing createCarPricingDto)
        {
            _carPricingDal.Add(createCarPricingDto);
            return new SuccessResult("Successfully added!");
        }

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

        public IDataResult<List<CarPricingDto>> GetCarPricingByCarId(int id)
        {
            var result = _carPricingDal.GetCarPricingByCarId(id);
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

        public IResult Update(CarPricing carPricing)
        {
            var result = _carPricingDal.Get(p => p.Id == carPricing.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Amount = carPricing.Amount;
                _carPricingDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }
    }
}
