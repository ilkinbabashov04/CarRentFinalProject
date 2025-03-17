using Core.DataAccess.Concrete;
using Core.Entity.Concrete;
using DataAccess.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfRoleManager : BaseReporsitory<AppRole, BaseProjectContext>, IRoleDal
    {
        public EfRoleManager(BaseProjectContext context) : base(context)
        {
        }

        public RolesDto GetRoleById(int id)
        {
            var context = new BaseProjectContext();
            var result = from a in context.AppUsers
                         where a.IsDelete == false
                         join r in context.AppRoles
                         on a.AppUserId equals id 
                         select new RolesDto
                         {
                             AppUserId = a.AppUserId,
                             RoleId = a.AppRoleId,
                             RoleName = r.AppRoleName,
                             UserName = a.UserName,
                         };
            return result.FirstOrDefault();
        }

        public List<RolesDto> GetRoles()
        {
            var context = new BaseProjectContext();
            var result = from a in context.AppUsers
                         where a.IsDelete == false
                         join r in context.AppRoles
                         on a.AppRoleId equals r.AppRoleId
                         select new RolesDto
                         {
                             AppUserId = a.AppUserId,
                             RoleId = r.AppRoleId,
                             RoleName = r.AppRoleName,
                             UserName = a.UserName,
                         };
            return result.ToList();
        }
    }
}
