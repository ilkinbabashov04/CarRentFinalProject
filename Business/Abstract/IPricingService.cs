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
	public interface IPricingService
	{
		IResult Add(Pricing pricing);
		IResult Update(Pricing pricing);
		IResult Delete(int id);
		IDataResult<Pricing> GetById(int id);
		IDataResult<List<Pricing>> GetAll();
		IDataResult<List<GetPricingByCarIdDto>> GetPricingByCarId(int carId);
	}
}
