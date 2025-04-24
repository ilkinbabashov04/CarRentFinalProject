using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfReviewDal : BaseReporsitory<Review, BaseProjectContext>, IReviewDal
	{
		public EfReviewDal(BaseProjectContext context) : base(context)
		{
		}

		public List<Review> GetReviewsByCarId(int id)
		{
			using (var context = new BaseProjectContext())
			{
				var result = from car in context.Cars
							 join review in context.Reviews
							 on car.Id equals review.CarId
							 where car.Id == id && car.IsDelete == false && review.IsDelete == false
							 select review;

				return result.ToList();
			}
		}
	}
}
