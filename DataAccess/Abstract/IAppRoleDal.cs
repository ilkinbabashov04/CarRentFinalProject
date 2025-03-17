using Core.DataAccess.Abstract;
using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAppRoleDal : IBaseReporsitory<AppRole>
    {
        Task<AppRole> GetByFilterAsyncc(Expression<Func<AppRole, bool>> filter);
    }
}
