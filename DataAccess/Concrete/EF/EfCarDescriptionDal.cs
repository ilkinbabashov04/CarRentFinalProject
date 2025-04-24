using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfCarDescriptionDal : BaseReporsitory<CarDescription, BaseProjectContext>, ICarDescriptionDal
	{
		public EfCarDescriptionDal(BaseProjectContext context) : base(context)
		{
		}

		public CarDescription GetCarDescriptionByCarId(int id)
		{
			using (var context = new BaseProjectContext())
			{
				var result = from car in context.Cars
							 join description in context.CarDescriptions
							 on car.Id equals description.CarId
							 where car.Id == id && car.IsDelete == false
							 select description;

				return result.FirstOrDefault();
			}
		}
	}
}
