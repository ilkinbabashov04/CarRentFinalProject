using Core.Entity.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspect.AppUserQuery
{
    public class GetCheckAppUserQuery : IRequest<GetCheckAppUser>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
