using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthorService
    {
        IResult Add(Author author);
        IResult Update(Author author);
        IResult Delete(int id);
        IDataResult<Author> GetById(int id);
        IDataResult<List<Author>> GetAll();
    }
}
