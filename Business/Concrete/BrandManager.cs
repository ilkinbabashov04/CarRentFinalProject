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
	public class BrandManager(IBrandDal brandDal) : IBrandService
	{
		private readonly IBrandDal _brandDal = brandDal;
		public IResult Add(string brand)
		{
			var result = new Brand
			{
				Name = brand
			};
			_brandDal.Add(result);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _brandDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_brandDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Brand>> GetAll()
		{
			var result = _brandDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Brand>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Brand>>(result, "Not found!");
			}
		}

		public IDataResult<Brand> GetById(int id)
		{
			var result = _brandDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Brand>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Brand>(result, "Not found!");
			}
		}

		public IResult Update(int id, string brandName)
		{
			var result = _brandDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = brandName;
				_brandDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
