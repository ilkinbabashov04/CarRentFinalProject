using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CreateCommentDto : IDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public int BlogId { get; set; }
    }
}
