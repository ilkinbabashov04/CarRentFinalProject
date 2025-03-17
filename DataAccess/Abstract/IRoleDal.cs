using Core.DataAccess.Abstract;
using Core.Entity.Concrete;
using DataAccess.Concrete.EF;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRoleDal : IBaseReporsitory<AppRole>
    {
        List<RolesDto> GetRoles();
        RolesDto GetRoleById(int id);
    }
}
