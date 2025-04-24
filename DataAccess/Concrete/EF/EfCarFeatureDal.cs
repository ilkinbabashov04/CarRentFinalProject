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
	public class EfCarFeatureDal : BaseReporsitory<CarFeature, BaseProjectContext>, ICarFeatureDal
	{
		public EfCarFeatureDal(BaseProjectContext context) : base(context)
		{
		}

        public void CarFeatureChangeAvailableToFalse(int id)
        {
            using (var context = new BaseProjectContext())
            {
                var carFeature = context.CarFeatures
                                        .FirstOrDefault(cf => cf.Id == id && cf.IsDelete == false);

                if (carFeature != null)
                {
                    carFeature.Available = false;
                    context.SaveChanges();
                }
            }
        }

        public void CarFeatureChangeAvailableToTrue(int id)
        {
            using (var context = new BaseProjectContext())
            {
                var carFeature = context.CarFeatures
                                        .FirstOrDefault(cf => cf.Id == id && cf.IsDelete == false);

                if (carFeature != null)
                {
                    carFeature.Available = true;
                    context.SaveChanges();
                }
            }
        }

        public List<CarFeatureDto> GetCarFeatureByCarId(int id)
        {
            var context = new BaseProjectContext();
            var result = from c in context.Cars
                         where c.Id == id && c.IsDelete == false
                         join cf in context.CarFeatures on c.Id equals cf.CarId
                         where cf.IsDelete == false
                         join f in context.Features on cf.FeatureId equals f.Id
                         where f.IsDelete == false
                         select new CarFeatureDto
                         {
                             Id = cf.Id,
                             FeatureId = f.Id,
                             FeatureName = f.Name,
                             Available = cf.Available,
                         };
            return result.ToList();

        }
    }
}
