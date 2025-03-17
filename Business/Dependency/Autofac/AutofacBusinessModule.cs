using Autofac;
using MediatR;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
//using Core.Helper.Interceptors;
//using Core.Helper.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity.Concrete;
using Business.BusinessAspect.AppUserQuery;

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
			builder.RegisterType<AuthorManager>().As<IAuthorService>().SingleInstance();
			builder.RegisterType<EfAuthorDal>().As<IAuthorDal>().SingleInstance();
			builder.RegisterType<BlogManager>().As<IBlogService>().SingleInstance();
			builder.RegisterType<EfBlogDal>().As<IBlogDal>().SingleInstance();
			builder.RegisterType<CarPricingManager>().As<ICarPricingService>().SingleInstance();
			builder.RegisterType<EfCarPricingDal>().As<ICarPricingDal>().SingleInstance();
			builder.RegisterType<TagCloudManager>().As<ITagCloudService>().SingleInstance();
			builder.RegisterType<EfTagCloudDal>().As<ITagCloudDal>().SingleInstance();
			builder.RegisterType<CommentManager>().As<ICommentService>().SingleInstance();
			builder.RegisterType<EfCommentDal>().As<ICommentDal>().SingleInstance();
			builder.RegisterType<EfAppUserDal>().As<IAppUserDal>().SingleInstance();
			builder.RegisterType<AppUserManager>().As<IAppUserService>().SingleInstance();
			builder.RegisterType<EfAppRoleDal>().As<IAppRoleDal>().SingleInstance();
			builder.RegisterType<AppRoleManager>().As<IAppRoleService>().SingleInstance();
			builder.RegisterType<RoleManager>().As<IRoleService>().SingleInstance();
			builder.RegisterType<EfRoleManager>().As<IRoleDal>().SingleInstance();
			builder.RegisterType<EfIAppUsersRoleIdDal>().As<IAppUsersRoleIdDal>().SingleInstance();
			builder.RegisterType<AppUsersRoleIdManager>().As<IAppUsersRoleIdService>().SingleInstance();
            builder.RegisterAssemblyTypes(typeof(CreateAppUserHandler).Assembly).AsImplementedInterfaces();

            //builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            //builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            //builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            //builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            //var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            //	.EnableInterfaceInterceptors(new ProxyGenerationOptions()
            //	{
            //		Selector = new AspectInterceptorSelector()
            //	}).SingleInstance();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Register MediatR handlers
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Register MediatR itself
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();



            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => componentContext.Resolve(t);
            //}).InstancePerLifetimeScope();
        }
	}
}
