using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AuthorManager(IAuthorDal authorDal) : IAuthorService
    {
        private readonly IAuthorDal _authorDal = authorDal;
        public IResult Add(Author author)
        {
            _authorDal.Add(author);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _authorDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _authorDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<Author>> GetAll()
        {
            var result = _authorDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Author>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<Author>>(result, "Not found!");
            }
        }

        public IDataResult<Author> GetById(int id)
        {
            var result = _authorDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<Author>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<Author>(result, "Not found!");
            }
        }

        public IResult Update(Author author)
        {
            var result = _authorDal.Get(p => p.Id == author.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Name = author.Name;
                result.Description = author.Description;
                result.ImageUrl = author.ImageUrl;
                _authorDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }
    }
}
