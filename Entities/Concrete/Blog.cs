using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        //public Author Author { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        //public Category Category { get; set; }
        public List<TagCloud> TagClouds { get; set; }
        //public List<Comment> Comments { get; set; }
    }
}
