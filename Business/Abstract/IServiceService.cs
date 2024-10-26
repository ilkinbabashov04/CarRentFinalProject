using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IServiceService
	{
		IResult Add(Service service);
		IResult Update(Service service);
		IResult Delete(int id);
		IDataResult<Service> GetById(int id);
		IDataResult<List<Service>> GetAll();
	}
}
