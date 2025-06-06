﻿using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IBrandService
	{
		IResult Add(string brand);
		IResult Update(int id, string brandName);
		IResult Delete(int id);
		IDataResult<Brand> GetById(int id);
		IDataResult<List<Brand>> GetAll();
	}
}
