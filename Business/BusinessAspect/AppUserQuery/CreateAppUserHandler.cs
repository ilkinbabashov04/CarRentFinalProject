using Core.Entity.Concrete;
using Core.Enum;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.AppUserQuery
{
    public class CreateAppUserHandler : IRequestHandler<CreateAppUser>
    {
        private readonly IAppUserDal _appUserDal;

        public CreateAppUserHandler(IAppUserDal appUserDal)
        {
            _appUserDal = appUserDal;
        }

        public async Task Handle(CreateAppUser request, CancellationToken cancellationToken)
        {
            await _appUserDal.CreateAsync(new AppUser
            {
                Password = request.Password,
                UserName = request.Username,
                AppRoleId = (int)RolesType.Member
            });
        }
    }
}
