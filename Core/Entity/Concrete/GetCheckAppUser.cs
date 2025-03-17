using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class GetCheckAppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
        public bool IsExist { get; set; }
    }
}
