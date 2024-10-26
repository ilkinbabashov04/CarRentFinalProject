using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CarManager(ICarDal carDal) : ICarService
	{
		private readonly ICarDal _carDal = carDal;
		public IResult Add(Car car)
		{
			//var result = new Car
			//{
			//	BrandId = car.BrandId,
			//	Model = car.Model,
			//	BigImageUrl = car.BigImageUrl,
			//	CoverImageUrl = car.CoverImageUrl,
			//	Fuel = car.Fuel,
			//	Km = car.Km,
			//	Luggage = car.Luggage,
			//	Transmission = car.Transmission,
			//	Seat = car.Seat,
			//};
			_carDal.Add(car);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _carDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_carDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<CarDto>> GetAll()
		{
			var result = _carDal.GetCarWithBrandName();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<CarDto>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<CarDto>>(result, "Not found!");
			}
		}

		public IDataResult<Car> GetById(int id)
		{
			var result = _carDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Car>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Car>(result, "Not found!");
			}
		}

        public IDataResult<List<CarDto>> GetFiveCars()
        {
			var result = _carDal.GetLastFiveCarsWithBrand();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<CarDto>>(result, "Not found!");
            }
        }

        public IResult Update(Car car)
		{
			var result = _carDal.Get(p => p.Id == car.Id && p.IsDelete == false);
			if (result != null)
			{
				result.BrandId = car.BrandId;
				result.Fuel = car.Fuel;
				result.Transmission = car.Transmission;
				result.Luggage = car.Luggage;
				result.Model = car.Model;
				result.Seat = car.Seat;
				result.Km = car.Km;
				result.BigImageUrl = car.BigImageUrl;
				result.CoverImageUrl = car.CoverImageUrl;
				_carDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
