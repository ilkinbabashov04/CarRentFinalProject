using Core.Entity.Concrete;
using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppUserService
    {
        IResult Add(AppUser about);
        IResult UpdateUserRole(int userId, int roleId);
        IResult Delete(int id);
        IDataResult<AppUser> GetById(int id);
        IDataResult<List<AppUser>> GetAll();
    }
}
