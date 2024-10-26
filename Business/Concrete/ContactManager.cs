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
	public class ContactManager(IContactDal contactDal) : IContactService
	{
		private readonly IContactDal _contactDal = contactDal;
		public IResult Add(Contact contact)
		{
			_contactDal.Add(contact);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _contactDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_contactDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Contact>> GetAll()
		{
			var result = _contactDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Contact>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Contact>>(result, "Not found!");
			}
		}

		public IDataResult<Contact> GetById(int id)
		{
			var result = _contactDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Contact>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Contact>(result, "Not found!");
			}
		}

		public IResult Update(Contact contact)
		{
			var result = _contactDal.Get(p => p.Id == contact.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = contact.Name;
				result.Subject = contact.Subject;
				result.Message = contact.Message;
				result.SendDate = contact.SendDate;
				result.Mail = contact.Mail;
				_contactDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
