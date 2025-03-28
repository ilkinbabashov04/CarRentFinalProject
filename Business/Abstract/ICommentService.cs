using Core.Helper.Result.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(Comment comment);
        IResult Update(Comment comment);
        IResult Delete(int id);
        IDataResult<Comment> GetById(int id);
        IDataResult<List<Comment>> GetAll();
        IDataResult<List<CommentDto>> GetCommentByBlogId(int id);
    }
}
