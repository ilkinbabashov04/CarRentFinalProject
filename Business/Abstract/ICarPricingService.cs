using Core.Helper.Result.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICarPricingService
	{
		IResult Add(CarPricing createCarPricingDto);
		IResult Update(CarPricing carPricing);
        IDataResult<List<CarPricingDto>> GetAll();
		IDataResult<List<GetCarPricingWithTimePeriodDto>> GetCarPricingWithTimePeriods();
		IDataResult<List<CarPricingDto>> GetCarPricingByCarId(int id);
    }
}
