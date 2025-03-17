using Business.Abstract;
//using Business.BusinessAspect.Autofac.Secured;
//using Business.Validation.FluentValidation;
//using Core.Aspect.Autofac.Validation.FluentValidation;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class SocialMediaManager(ISocialMediaDal socialMediaDal) : ISocialMediaService
	{
		private readonly ISocialMediaDal _socialMediaDal = socialMediaDal;
		public IResult Add(SocialMedia socialMedia)
		{
			_socialMediaDal.Add(socialMedia);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _socialMediaDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_socialMediaDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<SocialMedia>> GetAll()
		{
			var result = _socialMediaDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<SocialMedia>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<SocialMedia>>(result, "Not found!");
			}
		}

		public IDataResult<SocialMedia> GetById(int id)
		{
			var result = _socialMediaDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<SocialMedia>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<SocialMedia>(result, "Not found!");
			}
		}

		public IResult Update(SocialMedia socialMedia)
		{
			var result = _socialMediaDal.Get(p => p.Id == socialMedia.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = socialMedia.Name;
				result.Url = socialMedia.Url;
				result.IconUrl = socialMedia.IconUrl;
				_socialMediaDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
