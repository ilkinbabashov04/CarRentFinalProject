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
	}
}
