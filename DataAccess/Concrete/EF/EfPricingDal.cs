using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfPricingDal : BaseReporsitory<Pricing, BaseProjectContext>, IPricingDal
	{
		public EfPricingDal(BaseProjectContext context) : base(context)
		{
		}
	}
}
