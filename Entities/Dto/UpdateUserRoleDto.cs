using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UpdateUserRoleDto
    {
        public int AppUserId { get; set; }
        public int RoleId { get; set; }
    }
}
