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
	public class CategoryManager(ICategoryDal categoryDal ) : ICategoryService
	{
		private readonly ICategoryDal _categoryDal = categoryDal;
		public IResult Add(Category category)
		{
			_categoryDal.Add(category);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _categoryDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_categoryDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Category>> GetAll()
		{
			var result = _categoryDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Category>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Category>>(result, "Not found!");
			}
		}

		public IDataResult<Category> GetById(int id)
		{
			var result = _categoryDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Category>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Category>(result, "Not found!");
			}
		}

		public IResult Update(Category category)
		{
			var result = _categoryDal.Get(p => p.Id == category.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = category.Name;
				_categoryDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
