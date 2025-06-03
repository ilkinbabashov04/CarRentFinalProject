using Core.Entity.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BlogFileDto : IDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TagCloudDto>? TagClouds { get; set; }
    }
}
