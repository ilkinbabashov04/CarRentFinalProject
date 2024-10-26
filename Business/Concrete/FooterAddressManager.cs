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
    public class FooterAddressManager(IFooterAddressDal footerAddressDal) : IFooterAddressService
    {
		private readonly IFooterAddressDal _footerAddressDal = footerAddressDal;
		public IResult Add(FooterAddress footerAddress)
		{
			_footerAddressDal.Add(footerAddress);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _footerAddressDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_footerAddressDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<FooterAddress>> GetAll()
		{
			var result = _footerAddressDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<FooterAddress>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<FooterAddress>>(result, "Not found!");
			}
		}

		public IDataResult<FooterAddress> GetById(int id)
		{
			var result = _footerAddressDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<FooterAddress>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<FooterAddress>(result, "Not found!");
			}
		}

		public IResult Update(FooterAddress footerAddress)
		{
			var result = _footerAddressDal.Get(p => p.Id == footerAddress.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Address = footerAddress.Address;
				result.Phone = footerAddress.Phone;
				result.Description = footerAddress.Description;
				result.Mail = footerAddress.Mail;
				_footerAddressDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
