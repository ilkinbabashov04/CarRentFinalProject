using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dependency.Autofac
{
	public class AutofacBusinessModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<BaseProjectContext>().As<BaseProjectContext>().SingleInstance();
			builder.RegisterType<AboutManager>().As<IAboutService>().SingleInstance();
			builder.RegisterType<EfAboutDal>().As<IAboutDal>().SingleInstance();
			builder.RegisterType<HomeManager>().As<IHomeService>().SingleInstance();
			builder.RegisterType<EfHomeDal>().As<IHomeDal>().SingleInstance();
			builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
			builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
			builder.RegisterType<CarManager>().As<ICarService>().SingleInstance();
			builder.RegisterType<EfCarDal>().As<ICarDal>().SingleInstance();
			builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
			builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
			builder.RegisterType<ContactManager>().As<IContactService>().SingleInstance();
			builder.RegisterType<EfContactDal>().As<IContactDal>().SingleInstance();
			builder.RegisterType<FeatureManager>().As<IFeatureService>().SingleInstance();
			builder.RegisterType<EfFeatureDal>().As<IFeatureDal>().SingleInstance();
			builder.RegisterType<FooterAddressManager>().As<IFooterAddressService>().SingleInstance();
			builder.RegisterType<EfFooterAddressDal>().As<IFooterAddressDal>().SingleInstance();
			builder.RegisterType<LocationManager>().As<ILocationService>().SingleInstance();
			builder.RegisterType<EfLocationDal>().As<ILocationDal>().SingleInstance();
			builder.RegisterType<PricingManager>().As<IPricingService>().SingleInstance();
			builder.RegisterType<EfPricingDal>().As<IPricingDal>().SingleInstance();
			builder.RegisterType<ServiceManager>().As<IServiceService>().SingleInstance();
			builder.RegisterType<EfServiceDal>().As<IServiceDal>().SingleInstance();
			builder.RegisterType<SocialMediaManager>().As<ISocialMediaService>().SingleInstance();
			builder.RegisterType<EfSocialMediaDal>().As<ISocialMediaDal>().SingleInstance();
			builder.RegisterType<TestimonialManager>().As<ITestimonialService>().SingleInstance();
			builder.RegisterType<EfTestimaonialDal>().As<ITestimonialDal>().SingleInstance();
		}
	}
}
