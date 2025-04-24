using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper.Result.Concrete
{
	public class SuccessDataResult<T> : DataResult<T>
	{
		public SuccessDataResult(T data, string message) : base(data, true, message)
		{

		}
        public SuccessDataResult(List<Entity.Concrete.AppRole> result, T data) : base(data, true)
        {
            
        }
        public SuccessDataResult() : base(default, true) 
        {

        }
    }
}
