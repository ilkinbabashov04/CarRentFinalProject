using Business.Abstract;
using Core.Entity.Concrete;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppUserManager(IAppUserDal appUserDal) : IAppUserService
    {
        private readonly IAppUserDal _appUserDal = appUserDal;
        public IResult Add(AppUser appUser)
        {
            _appUserDal.Add(appUser);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _appUserDal.Get(p => p.AppUserId == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _appUserDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<AppUser>> GetAll()
        {
            var result = _appUserDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<AppUser>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<AppUser>>(result, "Not found!");
            }
        }

        public IDataResult<AppUser> GetById(int id)
        {
            var result = _appUserDal.Get(p => p.AppUserId == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<AppUser>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<AppUser>(result, "Not found!");
            }
        }

        public IResult UpdateUserRole(int userId, int roleId)
        {
            var user = _appUserDal.Get(p => p.AppUserId == userId && p.IsDelete == false);
            if (user != null)
            {
                user.AppRoleId = roleId;
                _appUserDal.Update(user);
                return new SuccessResult("User role updated successfully!");
            }
            else
            {
                return new ErrorResult("User not found!");
            }
        }

    }
}
