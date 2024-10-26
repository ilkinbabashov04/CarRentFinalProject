using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ISocialMediaService
	{
		IResult Add(SocialMedia about);
		IResult Update(SocialMedia about);
		IResult Delete(int id);
		IDataResult<SocialMedia> GetById(int id);
		IDataResult<List<SocialMedia>> GetAll();
	}
}
