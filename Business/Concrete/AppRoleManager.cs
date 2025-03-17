using Business.Abstract;
using Core.Entity.Concrete;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppRoleManager(IAppRoleDal appRoleDal) : IAppRoleService
    {
        private readonly IAppRoleDal _appRoleDal = appRoleDal;
        public IResult Add(AppRole AppRole)
        {
            _appRoleDal.Add(AppRole);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _appRoleDal.Get(p => p.AppRoleId == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _appRoleDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<AppRole>> GetAll()
        {
            var result = _appRoleDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<AppRole>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<AppRole>>(result, "Not found!");
            }
        }

        public IDataResult<AppRole> GetById(int id)
        {
            var result = _appRoleDal.Get(p => p.AppRoleId == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<AppRole>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<AppRole>(result, "Not found!");
            }
        }
    }
}
