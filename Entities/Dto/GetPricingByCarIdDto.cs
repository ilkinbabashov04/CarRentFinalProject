﻿using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class GetPricingByCarIdDto : IDto
    {
        public int CarId { get; set; }
        public int PricingId { get; set; }
        public decimal Amount { get; set; }
    }
}
