﻿using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICategoryService
	{
		IResult Add(Category category);
		IResult Update(Category category);
		IResult Delete(int id);
		IDataResult<Category> GetById(int id);
		IDataResult<List<Category>> GetAll();
	}
}
