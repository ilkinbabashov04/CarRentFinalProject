using Core.Helper.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IStatisticsService
    {
        IDataResult<GetCarCountDto> GetCarCount();
        IDataResult<GetAuthorCountDto> GetAuthorCount();
        IDataResult<GetLocationCountDto> GetLocationCount();
        IDataResult<GetBlogCountDto> GetBlogCount();
        IDataResult<GetBrandCountDto> GetBrandCount();
        IDataResult<GetAvrRentPriceForDailyDto> GetAvrRentPriceForDaily();
        IDataResult<GetAvrRentPriceForWeeklyDto> GetAvrRentPriceForWeekly();
        IDataResult<GetAvrRentPriceForMonthlyDto> GetAvrRentPriceForMonthly();
        IDataResult<GetCarCountByTransmissionIsAutoDto> GetCarCountByTransmissionIsAuto();
        IDataResult<GetCarCountByKmLessThan1000Dto> GetCarCountByKmLessThan1000();
        IDataResult<GetCarCountByFuelGasolineOrDieselDto> GetCarCountByFuelGasolineOrDiesel();
        IDataResult<GetCarCountByFuelElectricDto> GetCarCountByFuelElectric();
        IDataResult<GetCarBrandAndModelByDailyRentPriceIsMaxDto> GetCarBrandAndModelByDailyRentPriceIsMax();
        IDataResult<GetCarBrandAndModelByDailyRentPriceIsMinDto> GetCarBrandAndModelByDailyRentPriceIsMin();
        IDataResult<GetBrandNameByMaxCarDto> GetBrandNameByMaxCar();
        IDataResult<BlogTitleByMaxBlogCommentDto> BlogTitleByMaxBlogComment();
    }
}
