using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class GetCarCountByKmLessThan1000Dto : IDto
    {
        public int CarCount { get; set; }
    }
}
