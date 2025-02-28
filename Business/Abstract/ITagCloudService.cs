using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITagCloudService
    {
        IResult Add(TagCloud tagCloud);
        IResult Update(TagCloud tagCloud);
        IResult Delete(int id);
        IDataResult<TagCloud> GetById(int id);
        IDataResult<List<TagCloud>> GetAll();
        IDataResult<List<TagCloud>> GetTagCloudsByBlogId(int id);
    }
}
