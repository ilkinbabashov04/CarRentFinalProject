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
	public interface ICarFeatureService
	{
        IResult Add(CarFeature carFeature);
        IDataResult<List<CarFeatureDto>> GetCarFeatureByCarId(int id);
		IResult CarFeatureChangeAvailableToFalse(int id);
		IResult CarFeatureChangeAvailableToTrue(int id);

    }
}
