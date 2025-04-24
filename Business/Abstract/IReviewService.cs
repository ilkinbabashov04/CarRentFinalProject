using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IReviewService
	{
		IResult Add(Review review);
		IResult Update(Review review);
		IDataResult<Review> GetById(int id);
		IDataResult<List<Review>> GetReviewsByCarId(int id);
	}
}
