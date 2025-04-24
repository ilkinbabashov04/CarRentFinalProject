using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BarGraphDto : IDto
    {
        public string LocationName { get; set; }
        public int AvailableCarsCount { get; set; }
    }
}
