﻿using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IAboutService
	{
		IResult Add(About about);
		IResult Update(About about);
		IResult Delete(int id);
		IDataResult<About> GetById(int id);
		IDataResult<List<About>> GetAll();

	}
}