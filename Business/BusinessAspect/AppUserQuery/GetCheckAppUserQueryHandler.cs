using Business.Abstract;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.AppUserQuery
{
    public class GetCheckAppUserQueryHandler : IRequestHandler<GetCheckAppUserQuery, GetCheckAppUser>
    {
        private readonly IAppUserDal _appUserDal;
        private readonly IAppRoleDal _appRoleDal;
        public GetCheckAppUserQueryHandler(IAppUserDal appUserDal, IAppRoleDal appRoleDal)
        {
            _appUserDal = appUserDal;
            _appRoleDal = appRoleDal;
        }
        public async Task<GetCheckAppUser> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppUser();
            var user = await _appUserDal.GetByFilterAsyncc(x=> x.UserName == request.UserName && x.Password == request.Password && x.IsDelete == false);
            if(user == null)
            {
                values.IsExist = false;
            }
            else
            {
                values.IsExist = true;
                values.UserName = user.UserName;
                values.Roles = (await _appRoleDal.GetByFilterAsyncc(x => x.AppRoleId == user.AppRoleId)).AppRoleName;
                values.Id = user.AppUserId;
            }
            return values;
        }
    }
}
