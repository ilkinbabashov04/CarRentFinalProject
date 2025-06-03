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
    public class EfStatisticsDal : BaseReporsitory<Statistics, BaseProjectContext>, IStatisticsDal
    {
        public EfStatisticsDal(BaseProjectContext context) : base(context)
        {
        }

        public BlogTitleByMaxBlogCommentDto BlogTitleByMaxBlogComment()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Comments
                    .Join(context.Blogs,
                          comment => comment.BlogId,
                          blog => blog.Id,
                          (comment, blog) => new { comment, blog })
                    .Where(cb => cb.blog.IsDelete == false)
                    .GroupBy(cb => cb.blog.Id)
                    .Select(g => new
                    {
                        BlogId = g.Key,
                        CommentCount = g.Count()
                    })
                    .OrderByDescending(g => g.CommentCount)
                    .FirstOrDefault();

                if (result != null)
                {
                    var blogTitle = context.Blogs
                        .Where(b => b.Id == result.BlogId && b.IsDelete == false)
                        .Select(b => b.Title)
                        .FirstOrDefault();

                    return blogTitle != null
                        ? new BlogTitleByMaxBlogCommentDto { BlogTitle = blogTitle }
                        : new BlogTitleByMaxBlogCommentDto();
                }

                return new BlogTitleByMaxBlogCommentDto();
            }
        }

        public GetAuthorCountDto GetAuthorCount()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Authors.Count(c => !c.IsDelete);
                return new GetAuthorCountDto { AuthorCount = result };
            }
        }

        public GetAvrRentPriceForDailyDto GetAvrRentPriceForDaily()
        {
            using (var context = new BaseProjectContext())
            {
                var pricingId = context.Pricings
                    .Where(p => p.Name == "Per Day")
                    .Select(p => p.Id)
                    .FirstOrDefault();

                if (pricingId == 0) 
                    return new GetAvrRentPriceForDailyDto { AverageDailyRentPrice = 0 };

                var averageAmount = context.CarPricings
                    .Where(cp => cp.PricingId == pricingId)
                    .Select(cp => cp.Amount)
                    .AsEnumerable() 
                    .DefaultIfEmpty(0)
                    .Average();

                return new GetAvrRentPriceForDailyDto { AverageDailyRentPrice = averageAmount };
            }
        }

        public GetAvrRentPriceForMonthlyDto GetAvrRentPriceForMonthly()
        {
            using (var context = new BaseProjectContext())
            {
                var pricingId = context.Pricings
                    .Where(p => p.Name == "Per Month")
                    .Select(p => p.Id)
                    .FirstOrDefault();

                if (pricingId == 0)
                    return new GetAvrRentPriceForMonthlyDto { AverageMonthlyRentPrice = 0 };

                var averageAmount = context.CarPricings
                    .Where(cp => cp.PricingId == pricingId)
                    .Select(cp => cp.Amount)
                    .AsEnumerable() 
                    .DefaultIfEmpty(0)
                    .Average();

                return new GetAvrRentPriceForMonthlyDto { AverageMonthlyRentPrice = averageAmount };
            }
        }

        public GetAvrRentPriceForWeeklyDto GetAvrRentPriceForWeekly()
        {
            using (var context = new BaseProjectContext())
            {
                var pricingId = context.Pricings
                    .Where(p => p.Name == "Per Week")
                    .Select(p => p.Id)
                    .FirstOrDefault();

                if (pricingId == 0) 
                    return new GetAvrRentPriceForWeeklyDto { AverageWeeklyRentPrice = 0 };

                var averageAmount = context.CarPricings
                    .Where(cp => cp.PricingId == pricingId)
                    .Select(cp => cp.Amount)
                    .AsEnumerable() 
                    .DefaultIfEmpty(0)
                    .Average();

                return new GetAvrRentPriceForWeeklyDto { AverageWeeklyRentPrice = averageAmount };
            }
        }

        public GetBlogCountDto GetBlogCount()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Blogs.Count(c => !c.IsDelete);
                return new GetBlogCountDto { BlogCount = result };
            }
        }

        public GetBrandCountDto GetBrandCount()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Brands.Count(c => !c.IsDelete);
                return new GetBrandCountDto { BrandCount = result };
            }
        }

        public GetBrandNameByMaxCarDto GetBrandNameByMaxCar()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars
                    .Where(c => !c.IsDelete) // Exclude deleted cars
                    .GroupBy(c => c.BrandId)
                    .Select(g => new
                    {
                        BrandName = context.Brands.Where(b => b.Id == g.Key).Select(b => b.Name).FirstOrDefault(),
                        TotalCars = g.Count()
                    })
                    .OrderByDescending(g => g.TotalCars)
                    .FirstOrDefault();

                return result != null
                    ? new GetBrandNameByMaxCarDto { BrandName = result.BrandName }
                    : new GetBrandNameByMaxCarDto(); // Return an empty DTO if no data is found
            }
        }

        public GetCarBrandAndModelByDailyRentPriceIsMaxDto GetCarBrandAndModelByDailyRentPriceIsMax()
        {
            using (var context = new BaseProjectContext())
            {
                var pricingId = context.Pricings
                    .Where(p => p.Name == "Per Day")
                    .Select(p => p.Id)
                    .FirstOrDefault();

                var maxAmount = context.CarPricings
                    .Join(context.Cars.Where(c => !c.IsDelete),
                          cp => cp.CarId,
                          c => c.Id,
                          (cp, c) => new { cp.Amount, cp.PricingId })
                    .Where(cp => cp.PricingId == pricingId)
                    .Max(cp => (decimal?)cp.Amount) ?? 0;

                var result = context.CarPricings
                    .Join(context.Cars.Where(c => !c.IsDelete),
                          cp => cp.CarId,
                          c => c.Id,
                          (cp, c) => new { cp, c })
                    .Join(context.Brands,
                          cc => cc.c.BrandId,
                          b => b.Id,
                          (cc, b) => new GetCarBrandAndModelByDailyRentPriceIsMaxDto
                          {
                              CarModel = b.Name + " " + cc.c.Model,
                              CarId = cc.cp.CarId,
                              Amount = cc.cp.Amount
                          })
                    .Where(dto => dto.Amount == maxAmount)
                    .FirstOrDefault();

                return result ?? new GetCarBrandAndModelByDailyRentPriceIsMaxDto();
            }
        }

        public GetCarBrandAndModelByDailyRentPriceIsMinDto GetCarBrandAndModelByDailyRentPriceIsMin()
        {
            using (var context = new BaseProjectContext())
            {
                var pricingId = context.Pricings
                    .Where(p => p.Name == "Per Day")
                    .Select(p => p.Id)
                    .FirstOrDefault();

                var maxAmount = context.CarPricings
                    .Join(context.Cars.Where(c => !c.IsDelete),
                          cp => cp.CarId,
                          c => c.Id,
                          (cp, c) => new { cp.Amount, cp.PricingId })
                    .Where(cp => cp.PricingId == pricingId)
                    .Min(cp => (decimal?)cp.Amount) ?? 0;

                var result = context.CarPricings
                    .Join(context.Cars.Where(c => !c.IsDelete),
                          cp => cp.CarId,
                          c => c.Id,
                          (cp, c) => new { cp, c })
                    .Join(context.Brands,
                          cc => cc.c.BrandId,
                          b => b.Id,
                          (cc, b) => new GetCarBrandAndModelByDailyRentPriceIsMinDto
                          {
                              CarModel = b.Name + " " + cc.c.Model,
                              CarId = cc.cp.CarId,
                              Amount = cc.cp.Amount
                          })
                    .Where(dto => dto.Amount == maxAmount)
                    .FirstOrDefault();

                return result ?? new GetCarBrandAndModelByDailyRentPriceIsMinDto();
            }
        }

        public GetCarCountDto GetCarCount()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars.Count(c => !c.IsDelete);
                return new GetCarCountDto { CarCount = result };
            }
        }

        public GetCarCountByFuelElectricDto GetCarCountByFuelElectric()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars.Where(x => x.Fuel == "Electric" && x.IsDelete == false).Count();
                return new GetCarCountByFuelElectricDto { CarCountByFuelElectric = result };
            }
        }

        public GetCarCountByFuelGasolineOrDieselDto GetCarCountByFuelGasolineOrDiesel()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars.Where(x => x.Fuel == "Gasoline" || x.Fuel == "Diesel" && x.IsDelete == false).Count();
                return new GetCarCountByFuelGasolineOrDieselDto { CarCountByFuelGasolineOrDiesel = result };
            }
        }

        public GetCarCountByKmLessThan1000Dto GetCarCountByKmLessThan1000()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars.Where(x => x.Km <= 10000 && x.IsDelete == false).Count();
                return new GetCarCountByKmLessThan1000Dto { CarCount = result };
            }
        }

        public GetCarCountByTransmissionIsAutoDto GetCarCountByTransmissionIsAuto()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Cars.Where(x => x.Transmission == "Automatic" && x.IsDelete == false).Count();
                return new GetCarCountByTransmissionIsAutoDto { AutomaticCarCount = result };
            }
        }

        public GetLocationCountDto GetLocationCount()
        {
            using (var context = new BaseProjectContext())
            {
                var result = context.Locations.Count(c => !c.IsDelete);
                return new GetLocationCountDto { LocationCount = result };
            }
        }
    }
}
