using Core.DataAccess.Abstract;
using Core.Entity.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAppUserDal : IBaseReporsitory<AppUser>
    {
        Task<List<AppUser>> GetByFilterAsync(Expression<Func<AppUser, bool>> filter);
        Task<AppUser> GetByFilterAsyncc(Expression<Func<AppUser, bool>> filter);
        Task CreateAsync(AppUser entity);
        AppUser GetById(int id);
        void Update(AppUser appUser);

    }
}
