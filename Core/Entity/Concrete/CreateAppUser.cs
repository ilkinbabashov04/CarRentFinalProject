using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity.Concrete
{
    public class CreateAppUser : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
