using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IHomeService
	{
		IResult Add(Home home);
		IResult Update(Home home);
		IResult Delete(int id);
		IDataResult<Home> GetById(int id);
		IDataResult<List<Home>> GetAll();
	}
}
