using Core.Entity.Concrete;
using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppRoleService
    {
        IResult Add(AppRole appUser);
        IResult Delete(int id);
        IDataResult<AppRole> GetById(int id);
        IDataResult<List<AppRole>> GetAll();
    }
}
