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
	public class EfAboutDal : BaseReporsitory<About, BaseProjectContext>, IAboutDal
	{
		public EfAboutDal(BaseProjectContext context) : base(context)
		{
		}
	}
}
