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
	public class ServiceManager(IServiceDal serviceDal) : IServiceService
	{
		private readonly IServiceDal _serviceDal = serviceDal;
		public IResult Add(Service service)
		{
			_serviceDal.Add(service);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _serviceDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_serviceDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Service>> GetAll()
		{
			var result = _serviceDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Service>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Service>>(result, "Not found!");
			}
		}

		public IDataResult<Service> GetById(int id)
		{
			var result = _serviceDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Service>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Service>(result, "Not found!");
			}
		}

		public IResult Update(Service service)
		{
			var result = _serviceDal.Get(p => p.Id == service.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Title = service.Title;
				result.Description = service.Description;
				result.IconUrl = service.IconUrl;
				_serviceDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
