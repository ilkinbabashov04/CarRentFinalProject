using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class PieChartDto : IDto
    {
        public string BrandName { get; set; }
        public int Count { get; set; }
    }
}
