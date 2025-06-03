using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICarDescriptionService
	{
        IResult Update(CarDescription carDescription);
        IResult Add(CarDescription carDescription);
        IResult GetCarDescriptionByCarId(int id);

    }
}
