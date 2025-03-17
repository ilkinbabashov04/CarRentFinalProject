using Business.Abstract;
using Core.Entity.Concrete;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager(IRoleDal roleDal, IAppUserDal appUserDal) : IRoleService
    {
        private readonly IRoleDal _roleDal = roleDal;
        private readonly IAppUserDal _appUserDal = appUserDal;
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

        public IDataResult<List<RolesDto>> GetAll()
        {
            var result = _roleDal.GetRoles().ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<RolesDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<RolesDto>>(result, "Not found!");
            }
        }

        public IDataResult<RolesDto> GetById(int id)
        {
            var result = _roleDal.GetRoleById(id);
            if (result != null)
            {
                return new SuccessDataResult<RolesDto>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<RolesDto>(result, "Not found!");
            }
        }

        //public IResult Update(AppRole role)
        //{
        //    var result = _roleDal.Get(p => p.AppRoleId == role.AppRoleId && p.IsDelete == false);
        //    if (result != null)
        //    {
        //        result.AppRoleId = role.AppRoleId;
        //        return new SuccessResult("Updated successfully!");
        //    }
        //    else
        //    {
        //        return new ErrorResult("Not found!");
        //    }
        //}
    }
}
