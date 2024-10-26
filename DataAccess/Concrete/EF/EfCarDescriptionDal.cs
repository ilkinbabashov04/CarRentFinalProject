using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
	public class EfCarDescriptionDal : BaseReporsitory<CarDescription, BaseProjectContext>, ICarDescriptionDal
	{
		public EfCarDescriptionDal(BaseProjectContext context) : base(context)
		{
		}
	}
}
