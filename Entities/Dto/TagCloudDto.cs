using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class TagCloudDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BlogId { get; set; }
    }
}
