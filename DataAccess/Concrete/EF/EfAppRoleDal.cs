using Core.DataAccess.Concrete;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfAppRoleDal : BaseReporsitory<AppRole, BaseProjectContext>, IAppRoleDal
    {
        private readonly BaseProjectContext _context;
        public EfAppRoleDal(BaseProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppRole> GetByFilterAsyncc(Expression<Func<AppRole, bool>> filter)
        {
            return await _context.Set<AppRole>().SingleOrDefaultAsync(filter);
        }
    }
}
