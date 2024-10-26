using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
	public abstract class BaseEntity : IEntity
	{
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
