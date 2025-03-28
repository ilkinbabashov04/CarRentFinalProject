using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;

namespace DataAccess.Concrete.EF
{
    public class EfBlogDal : BaseReporsitory<Blog, BaseProjectContext>, IBlogDal
    {
        public EfBlogDal(BaseProjectContext context) : base(context)
        {
        }

        public List<BlogDto> GetAllBlogsWithAuthor()
        {
            var context = new BaseProjectContext();
            var values = from b in context.Blogs
                         where b.IsDelete == false
                         join c in context.Categories
                         on b.CategoryId equals c.Id
                         join a in context.Authors
                         on b.AuthorId equals a.Id
                         select new BlogDto
                         {
                             Id = b.Id,
                             Title = b.Title,
                             AuthorName = a.Name,
                             AuthorImageUrl = a.ImageUrl,
                             AuthorDescription = a.Description,
                             CategoryName = c.Name,
                             AuthorId = a.Id,
                             CoverImageUrl = b.CoverImageUrl,
                             CreatedDate = b.CreatedDate,
                             CategoryId = c.Id,
                             Description = b.Description,
                         };
            return values.ToList();
        }

        public List<BlogWithAuthorIdDto> GetBlogWithoutAuthorId(int id)
        {
            var context = new BaseProjectContext();
            var values = from b in context.Blogs
                         where b.Id == id
                         where b.IsDelete == false
                         join a in context.Authors
                         on b.AuthorId equals a.Id
                         select new BlogWithAuthorIdDto
                         {
                             AuthorId = a.Id,
                             BlogId = b.Id,
                             AuthorName = a.Name,
                             AuthorImageUrl = a.ImageUrl,
                             AuthorDescription = a.Description,
                         };
            return values.ToList();
        }

        public List<BlogDto> GetLastThreeBlogs()
        {
            var context = new BaseProjectContext();
            var values = from b in context.Blogs
                         where b.IsDelete == false
                         join c in context.Categories
                         on b.CategoryId equals c.Id
                         join a in context.Authors
                         on b.AuthorId equals a.Id
                         select new BlogDto
                         {
                             AuthorId = a.Id,
                             AuthorName = a.Name,
                             CategoryId = b.Id,
                             CoverImageUrl = b.CoverImageUrl,
                             CreatedDate = b.CreatedDate,
                             Title = b.Title,
                         };
            return values.Take(3).ToList();
        }
    }
}
