using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IFooterAddressService
	{
		IResult Add(FooterAddress footerAddress);
		IResult Update(FooterAddress footerAddress);
		IResult Delete(int id);
		IDataResult<FooterAddress> GetById(int id);
		IDataResult<List<FooterAddress>> GetAll();
	}
}
