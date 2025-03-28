using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class PricingDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
