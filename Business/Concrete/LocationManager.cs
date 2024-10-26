using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class LocationManager(ILocationDal locationDal) : ILocationService
	{
		private readonly ILocationDal _locationDal = locationDal;
		public IResult Add(Location location)
		{
			_locationDal.Add(location);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _locationDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_locationDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Location>> GetAll()
		{
			var result = _locationDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Location>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Location>>(result, "Not found!");
			}
		}

		public IDataResult<Location> GetById(int id)
		{
			var result = _locationDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Location>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Location>(result, "Not found!");
			}
		}

		public IResult Update(Location location)
		{
			var result = _locationDal.Get(p => p.Id == location.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = location.Name;
				_locationDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
