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
	public class AboutManager(IAboutDal aboutDal) : IAboutService
	{
		private readonly IAboutDal _aboutDal = aboutDal;
		public IResult Add(About about)
		{
			_aboutDal.Add(about);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _aboutDal.Get(p=> p.Id == id && p.IsDelete == false);
			if(result != null)
			{
				result.IsDelete = true;
				_aboutDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<About>> GetAll()
		{
			var result = _aboutDal.GetAll(p => p.IsDelete == false).ToList();
			if(result.Count > 0)
			{
				return new SuccessDataResult<List<About>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<About>>(result, "Not found!");
			}
		}

		public IDataResult<About> GetById(int id)
		{
			var result = _aboutDal.Get(p => p.Id == id && p.IsDelete ==false);
			if(result != null)
			{
				return new SuccessDataResult<About>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<About>(result, "Not found!");
			}
		}

		public IResult Update(About about)
		{
			var result = _aboutDal.Get(p => p.Id == about.Id && p.IsDelete == false);
			if(result != null)
			{
				result.Title = about.Title;
				result.Description = about.Description;
				result.ImageUrl = about.ImageUrl;
				_aboutDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
