﻿using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IPricingDal : IBaseReporsitory<Pricing>
	{
		List<GetPricingByCarIdDto> GetPricingByCarId(int id);
	}
}
