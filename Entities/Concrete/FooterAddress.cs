using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class FooterAddress : BaseEntity
	{
        public string Description { get; set; }
		public string Address { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
    }
}
