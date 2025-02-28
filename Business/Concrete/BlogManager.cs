using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;

namespace Business.Concrete
{
    public class BlogManager(IBlogDal blogDal) : IBlogService
    {
        private readonly IBlogDal _blogDal = blogDal;
        public IResult Add(Blog blog)
        {
            _blogDal.Add(blog);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _blogDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _blogDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<Blog>> GetAll()
        {
            var result = _blogDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Blog>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<Blog>>(result, "Not found!");
            }
        }

        public IDataResult<List<BlogDto>> GetAllBlogsWithAuthor()
        {
            var result = _blogDal.GetAllBlogsWithAuthor();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<BlogDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<BlogDto>>(result, "Not found!");
            }
        }

        public IDataResult<List<BlogWithAuthorIdDto>> GetBlogWithoutAuthorId(int id)
        {
            var result = _blogDal.GetBlogWithoutAuthorId(id);
            if (result != null)
            {
                return new SuccessDataResult<List<BlogWithAuthorIdDto>>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<List<BlogWithAuthorIdDto>>(result, "Not found!");
            }
        }

        public IDataResult<Blog> GetById(int id)
        {
            var result = _blogDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<Blog>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<Blog>(result, "Not found!");
            }
        }

        public IDataResult<List<BlogDto>> GetLastThreeBlogs()
        {
            var result = _blogDal.GetLastThreeBlogs();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<BlogDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<BlogDto>>(result, "Not found!");
            }
        }

        public IResult Update(Blog blog)
        {
            var result = _blogDal.Get(p => p.Id == blog.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Title = blog.Title;
                result.AuthorId = blog.AuthorId;
                result.CategoryId = blog.CategoryId;
                result.CoverImageUrl = blog.CoverImageUrl;
                result.CreatedDate = blog.CreatedDate;
                result.Description = blog.Description;
                _blogDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

    }
}
