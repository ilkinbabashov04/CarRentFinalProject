using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class GetBlogCountDto : IDto
    {
        public int BlogCount { get; set; }
    }
}
