using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BlogWithAuthorIdDto : IDto
    {
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public string AuthorDescription { get; set; }
    }
}
