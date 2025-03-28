using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfCommentDal : BaseReporsitory<Comment, BaseProjectContext>, ICommentDal
    {
        public EfCommentDal(BaseProjectContext context) : base(context)
        {
        }

        public List<CommentDto> GetCommentsByBlogId(int id)
        {
            var context = new BaseProjectContext();
            var result = from c in context.Comments
                         where c.IsDelete == false
                         where c.BlogId == id
                         select new CommentDto
                         {
                             Id = c.Id,
                             BlogId = c.BlogId,
                             CreatedDate = c.CreatedDate,
                             Description = c.Description,
                             Name = c.Name,
                         };
            return result.ToList();
        }
    }
}
