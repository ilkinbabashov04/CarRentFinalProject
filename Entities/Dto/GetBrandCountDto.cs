using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class GetBrandCountDto : IDto
    {
        public int BrandCount { get; set; }
    }
}
