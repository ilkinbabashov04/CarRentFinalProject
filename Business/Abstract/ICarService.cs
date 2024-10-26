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
	public interface ICarService
	{
		IResult Add(Car car);
		IResult Update(Car car);
		IResult Delete(int id);
		IDataResult<Car> GetById(int id);
		IDataResult<List<CarDto>> GetAll();
		IDataResult<List<CarDto>> GetFiveCars();
	}
}
