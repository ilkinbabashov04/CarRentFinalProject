using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppUsersRoleIdManager(IAppUsersRoleIdDal appUsersRoleIdDal) : IAppUsersRoleIdService
    {
        private readonly IAppUsersRoleIdDal _appUsersRoleIdDal = appUsersRoleIdDal;
        public IDataResult<AppUsersRoleId> GetById(int id)
        {
            var result = _appUsersRoleIdDal.GetById(id);
            if (result != null)
            {
                return new SuccessDataResult<AppUsersRoleId>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<AppUsersRoleId>(result, "Not found!");
            }
        }
    }
}
