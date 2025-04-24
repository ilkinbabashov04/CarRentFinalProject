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
    public interface IStatisticsDal : IBaseReporsitory<Statistics>
    {
        GetCarCountDto GetCarCount();
        GetLocationCountDto GetLocationCount();
        GetAuthorCountDto GetAuthorCount();
        GetBlogCountDto GetBlogCount();
        GetBrandCountDto GetBrandCount();
        GetAvrRentPriceForDailyDto GetAvrRentPriceForDaily();
        GetAvrRentPriceForWeeklyDto GetAvrRentPriceForWeekly();
        GetAvrRentPriceForMonthlyDto GetAvrRentPriceForMonthly();
        GetCarCountByTransmissionIsAutoDto GetCarCountByTransmissionIsAuto();
        GetCarCountByKmLessThan1000Dto GetCarCountByKmLessThan1000();
        GetCarCountByFuelGasolineOrDieselDto GetCarCountByFuelGasolineOrDiesel();
        GetCarCountByFuelElectricDto GetCarCountByFuelElectric();
        GetCarBrandAndModelByDailyRentPriceIsMaxDto GetCarBrandAndModelByDailyRentPriceIsMax();
        GetCarBrandAndModelByDailyRentPriceIsMinDto GetCarBrandAndModelByDailyRentPriceIsMin();
        GetBrandNameByMaxCarDto GetBrandNameByMaxCar();
        BlogTitleByMaxBlogCommentDto BlogTitleByMaxBlogComment();
    }
}
