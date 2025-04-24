using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Concrete
{
    public class StatisticsManager(IStatisticsDal statisticsDal) : IStatisticsService
    {
        private readonly IStatisticsDal _statisticsDal = statisticsDal;

        public IDataResult<BlogTitleByMaxBlogCommentDto> BlogTitleByMaxBlogComment()
        {
            var result = _statisticsDal.BlogTitleByMaxBlogComment();
            if (result != null)
            {
                return new SuccessDataResult<BlogTitleByMaxBlogCommentDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<BlogTitleByMaxBlogCommentDto>(result, "Not found!");
            }
        }

        public IDataResult<GetAuthorCountDto> GetAuthorCount()
        {
            var result = _statisticsDal.GetAuthorCount();
            if (result.AuthorCount > 0)
            {
                return new SuccessDataResult<GetAuthorCountDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetAuthorCountDto>(result, "Not found!");
            }
        }

        public IDataResult<GetAvrRentPriceForDailyDto> GetAvrRentPriceForDaily()
        {
            var result = _statisticsDal.GetAvrRentPriceForDaily();
            if (result != null)
            {
                return new SuccessDataResult<GetAvrRentPriceForDailyDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetAvrRentPriceForDailyDto>(result, "Not found!");
            }
        }

        public IDataResult<GetAvrRentPriceForMonthlyDto> GetAvrRentPriceForMonthly()
        {
            var result = _statisticsDal.GetAvrRentPriceForMonthly();
            if (result != null)
            {
                return new SuccessDataResult<GetAvrRentPriceForMonthlyDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetAvrRentPriceForMonthlyDto>(result, "Not found!");
            }
        }

        public IDataResult<GetAvrRentPriceForWeeklyDto> GetAvrRentPriceForWeekly()
        {
            var result = _statisticsDal.GetAvrRentPriceForWeekly();
            if (result != null)
            {
                return new SuccessDataResult<GetAvrRentPriceForWeeklyDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetAvrRentPriceForWeeklyDto>(result, "Not found!");
            }
        }

        public IDataResult<GetBlogCountDto> GetBlogCount()
        {
            var result = _statisticsDal.GetBlogCount();
            if (result.BlogCount > 0)
            {
                return new SuccessDataResult<GetBlogCountDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetBlogCountDto>(result, "Not found!");
            }
        }

        public IDataResult<GetBrandCountDto> GetBrandCount()
        {
            var result = _statisticsDal.GetBrandCount();
            if (result.BrandCount > 0)
            {
                return new SuccessDataResult<GetBrandCountDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetBrandCountDto>(result, "Not found!");
            }
        }

        public IDataResult<GetBrandNameByMaxCarDto> GetBrandNameByMaxCar()
        {
            var result = _statisticsDal.GetBrandNameByMaxCar();
            if (result != null)
            {
                return new SuccessDataResult<GetBrandNameByMaxCarDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetBrandNameByMaxCarDto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarBrandAndModelByDailyRentPriceIsMaxDto> GetCarBrandAndModelByDailyRentPriceIsMax()
        {
            var result = _statisticsDal.GetCarBrandAndModelByDailyRentPriceIsMax();
            if (result != null)
            {
                return new SuccessDataResult<GetCarBrandAndModelByDailyRentPriceIsMaxDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarBrandAndModelByDailyRentPriceIsMaxDto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarBrandAndModelByDailyRentPriceIsMinDto> GetCarBrandAndModelByDailyRentPriceIsMin()
        {
            var result = _statisticsDal.GetCarBrandAndModelByDailyRentPriceIsMin();
            if (result != null)
            {
                return new SuccessDataResult<GetCarBrandAndModelByDailyRentPriceIsMinDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarBrandAndModelByDailyRentPriceIsMinDto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarCountByFuelElectricDto> GetCarCountByFuelElectric()
        {
            var result = _statisticsDal.GetCarCountByFuelElectric();
            if (result.CarCountByFuelElectric > 0)
            {
                return new SuccessDataResult<GetCarCountByFuelElectricDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarCountByFuelElectricDto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarCountByFuelGasolineOrDieselDto> GetCarCountByFuelGasolineOrDiesel()
        {
            var result = _statisticsDal.GetCarCountByFuelGasolineOrDiesel();
            if (result.CarCountByFuelGasolineOrDiesel > 0)
            {
                return new SuccessDataResult<GetCarCountByFuelGasolineOrDieselDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarCountByFuelGasolineOrDieselDto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarCountByKmLessThan1000Dto> GetCarCountByKmLessThan1000()
        {
            var result = _statisticsDal.GetCarCountByKmLessThan1000();
            if (result.CarCount > 0)
            {
                return new SuccessDataResult<GetCarCountByKmLessThan1000Dto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarCountByKmLessThan1000Dto>(result, "Not found!");
            }
        }

        public IDataResult<GetCarCountByTransmissionIsAutoDto> GetCarCountByTransmissionIsAuto()
        {
            var result = _statisticsDal.GetCarCountByTransmissionIsAuto();
            if (result.AutomaticCarCount > 0)
            {
                return new SuccessDataResult<GetCarCountByTransmissionIsAutoDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarCountByTransmissionIsAutoDto>(result, "Not found!");
            }
        }

        public IDataResult<GetLocationCountDto> GetLocationCount()
        {
            var result = _statisticsDal.GetLocationCount();
            if (result.LocationCount > 0)
            {
                return new SuccessDataResult<GetLocationCountDto>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetLocationCountDto>(result, "Not found!");
            }
        }

        IDataResult<GetCarCountDto> IStatisticsService.GetCarCount()
        {
            var result = _statisticsDal.GetCarCount();
            if (result.CarCount > 0)
            {
                return new SuccessDataResult<GetCarCountDto> (result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<GetCarCountDto>(result, "Not found!");
            }
        }
    }
}
