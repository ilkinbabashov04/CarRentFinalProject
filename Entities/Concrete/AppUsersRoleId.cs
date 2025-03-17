using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AppUsersRoleId
    {
        public bool IsDelete { get; set; } = false;
        public int AppUserId { get; set; }
        public int RoleId { get; set; }
    }
}
