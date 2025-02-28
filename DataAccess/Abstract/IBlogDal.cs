using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBlogDal : IBaseReporsitory<Blog>
    {
        List<BlogDto> GetLastThreeBlogs();
        List<BlogDto> GetAllBlogsWithAuthor();
        List<BlogWithAuthorIdDto> GetBlogWithoutAuthorId(int id);
    }
}
