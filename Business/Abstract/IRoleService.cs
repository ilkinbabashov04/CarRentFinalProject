using Core.Entity.Concrete;
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
    public interface IRoleService
    {
        //IResult Update(AppRole role);
        IDataResult<List<RolesDto>> GetAll();
        IResult Delete (int id);
        IDataResult<RolesDto> GetById(int id);
    }
}
