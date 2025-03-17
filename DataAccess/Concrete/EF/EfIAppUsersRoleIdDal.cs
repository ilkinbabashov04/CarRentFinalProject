using Core.DataAccess.Concrete;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfIAppUsersRoleIdDal : BaseReporsitory<AppUsersRoleId, BaseProjectContext>, IAppUsersRoleIdDal
    {
        public EfIAppUsersRoleIdDal(BaseProjectContext context) : base(context)
        {
        }

        public AppUsersRoleId GetById(int id)
        {
            var context = new BaseProjectContext();
            var result = from a in context.AppUsers
                         where a.IsDelete == false
                         join r in context.AppRoles
                         on a.AppUserId equals id
                         select new AppUsersRoleId
                         {
                             AppUserId = a.AppUserId,
                             RoleId = a.AppRoleId,
                         };
            return result.FirstOrDefault();
        }
    }
}
