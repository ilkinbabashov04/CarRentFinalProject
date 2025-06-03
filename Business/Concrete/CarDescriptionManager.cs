using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarDescriptionManager(ICarDescriptionDal carDescriptionDal) : ICarDescriptionService
    {
        ICarDescriptionDal _carDescriptionDal = carDescriptionDal;

        public IResult Add(CarDescription carDescription)
        {
            _carDescriptionDal.Add(carDescription);
            return new SuccessResult("Successfully added!");
        }
        public IResult GetCarDescriptionByCarId(int id)
		{
			var result = _carDescriptionDal.GetCarDescriptionByCarId(id);
			if (result != null)
			{
				return new SuccessDataResult<CarDescription>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<CarDescription>(result, "Not found!");
			}
		}
        public IResult Update(CarDescription carDescription)
        {
            var result = _carDescriptionDal.Get(p => p.Id == carDescription.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Detail = carDescription.Detail;
                _carDescriptionDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }
    }
}
