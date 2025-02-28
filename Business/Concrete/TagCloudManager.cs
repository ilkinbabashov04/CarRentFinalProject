using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TagCloudManager(ITagCloudDal tagCloudDal) : ITagCloudService
    {
        private readonly ITagCloudDal _tagCloudDal = tagCloudDal;
        public IResult Add(TagCloud tagCloud)
        {
            _tagCloudDal.Add(tagCloud);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _tagCloudDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _tagCloudDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found");
            }
        }

        public IDataResult<List<TagCloud>> GetAll()
        {
            var result = _tagCloudDal.GetAll(p=> p.IsDelete == false).ToList();
            if(result.Count > 0)
            {
                return new SuccessDataResult<List<TagCloud>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<TagCloud>>(result, "Not found!");
            }

        }

        public IDataResult<TagCloud> GetById(int id)
        {
            var result = _tagCloudDal.Get(p => p.Id == id && p.IsDelete==false);
            if(result != null)
            {
                return new SuccessDataResult<TagCloud>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<TagCloud>(result, "Not found!");
            }
        }

        public IDataResult<List<TagCloud>> GetTagCloudsByBlogId(int id)
        {
            var result = _tagCloudDal.GetAll(p=> p.BlogId == id).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<TagCloud>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<TagCloud>>(result, "Not found!");
            }
        }

        public IResult Update(TagCloud tagCloud)
        {
            var result = _tagCloudDal.Get(p=> p.Id  == tagCloud.Id && p.IsDelete == false);
            if(result != null)
            {
                result.Title = tagCloud.Title;
                result.BlogId = tagCloud.BlogId;
                _tagCloudDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }

        }
    }
}
