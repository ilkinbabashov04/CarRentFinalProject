using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class TokenResponseDto
    {
        public TokenResponseDto(string token, DateTime expireDate, string role)
        {
            Token = token;
            ExpireDate = expireDate;
            Role = role;
        }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Role { get; set; }
    }
}
