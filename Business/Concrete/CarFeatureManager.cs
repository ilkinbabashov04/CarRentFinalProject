using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarFeatureManager(ICarFeatureDal carFeatureDal) : ICarFeatureService
    {
        private readonly ICarFeatureDal _carFeatureDal = carFeatureDal;

        public IResult Add(CarFeature carFeature)
        {
            _carFeatureDal.Add(carFeature);
            return new SuccessResult("Successfully added!");
        }

        public IResult CarFeatureChangeAvailableToFalse(int id)
        {
            _carFeatureDal.CarFeatureChangeAvailableToFalse(id);
            return new SuccessResult("Successfully added!");
        }

        public IResult CarFeatureChangeAvailableToTrue(int id)
        {
            _carFeatureDal.CarFeatureChangeAvailableToTrue(id);
            return new SuccessResult("Successfully added!");
        }

        public IDataResult<List<CarFeatureDto>> GetCarFeatureByCarId(int id)
        {
            var result = _carFeatureDal.GetCarFeatureByCarId(id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarFeatureDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<CarFeatureDto>>(result, "Not found!");
            }
        }
    }
}
