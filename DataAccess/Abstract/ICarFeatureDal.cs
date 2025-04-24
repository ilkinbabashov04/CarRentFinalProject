using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface ICarFeatureDal : IBaseReporsitory<CarFeature>
	{
		List<CarFeatureDto> GetCarFeatureByCarId(int id);
		void CarFeatureChangeAvailableToFalse(int id);
		void CarFeatureChangeAvailableToTrue(int id);
	}
}
