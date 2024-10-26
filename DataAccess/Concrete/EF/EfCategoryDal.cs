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
	public class EfCategoryDal : BaseReporsitory<Category, BaseProjectContext>, ICategoryDal
	{
		public EfCategoryDal(BaseProjectContext context) : base(context)
		{
		}
	}
}
