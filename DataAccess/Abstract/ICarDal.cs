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
	public interface ICarDal : IBaseReporsitory<Car>
	{
		List<CarDto> GetCarWithBrandName();
		List<CarDto> GetLastFiveCarsWithBrand();
		List<PieChartDto> GetPieChartDetail();
		List<CarDto> GetAvailableCars(int locationId, DateTime pickupDateTime, DateTime dropoffDateTime);

    }
}
