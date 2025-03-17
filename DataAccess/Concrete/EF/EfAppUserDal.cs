using Core.DataAccess.Concrete;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfAppUserDal : BaseReporsitory<AppUser, BaseProjectContext>, IAppUserDal
    {
        private readonly BaseProjectContext _context;
        public EfAppUserDal(BaseProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateAsync(AppUser entity)
        {
            _context.Set<AppUser>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AppUser>> GetByFilterAsync(Expression<Func<AppUser, bool>> filter)
        {
            var values = await _context.AppUsers.Where(filter).ToListAsync();
            return values;
        }

        public async Task<AppUser> GetByFilterAsyncc(Expression<Func<AppUser, bool>> filter)
        {
            return await _context.Set<AppUser>().SingleOrDefaultAsync(filter);
        }

        public AppUser GetById(int id)
        {
            var context = new BaseProjectContext();
            var result = from a in context.AppUsers
                         where a.IsDelete == false
                         join r in context.AppRoles
                         on a.AppUserId equals id
                         select new AppUser
                         {
                             AppUserId = id,
                             AppRoleId = r.AppRoleId,
                         };
            return result.First();
        }
    }
}
