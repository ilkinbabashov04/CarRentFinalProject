using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CommentManager(ICommentDal commentDal) : ICommentService
    {
        private readonly ICommentDal _commentDal = commentDal;
        public IResult Add(Comment comment)
        {
            _commentDal.Add(comment);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _commentDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _commentDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<Comment>> GetAll()
        {
            var result = _commentDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Comment>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<Comment>>(result, "Not found!");
            }
        }

        public IDataResult<Comment> GetById(int id)
        {
            var result = _commentDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<Comment>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<Comment>(result, "Not found!");
            }
        }

        public IDataResult<List<CommentDto>> GetCommentByBlogId(int id)
        {
            var result = _commentDal.GetCommentsByBlogId(id);
            if (result != null)
            {
                return new SuccessDataResult<List<CommentDto>>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<List<CommentDto>>(result, "Not found!");
            }
        }

        public IResult Update(Comment comment)
        {
            var result = _commentDal.Get(p => p.Id == comment.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Name = comment.Name;
                result.CreatedDate = comment.CreatedDate;
                result.BlogId = comment.BlogId;
                result.Description = comment.Description;
                _commentDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }
    }
}
