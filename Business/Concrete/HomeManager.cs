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
	public class HomeManager(IHomeDal homeDal) : IHomeService
	{
		private readonly IHomeDal _homeDal = homeDal;
		public IResult Add(Home home)
		{
			_homeDal.Add(home);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _homeDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_homeDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Home>> GetAll()
		{
			var result = _homeDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Home>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Home>>(result, "Not found!");
			}
		}

		public IDataResult<Home> GetById(int id)
		{
			var result = _homeDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Home>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Home>(result, "Not found!");
			}
		}

		public IResult Update(Home home)
		{
			var result = _homeDal.Get(p => p.Id == home.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Title = home.Title;
				result.Description = home.Description;
				result.VideoDescription = home.VideoDescription;
				result.VideoUrl = home.VideoUrl;
				_homeDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
