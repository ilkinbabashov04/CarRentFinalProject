using Core.DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IReviewDal : IBaseReporsitory<Review>
	{
		List<Review> GetReviewsByCarId(int id);
	}
}
