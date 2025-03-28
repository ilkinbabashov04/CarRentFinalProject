using Core.Helper.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRentACarService
    {
        IDataResult<List<CarGetByFilterDto>> GetByFilter(int locationId, bool available);
    }
}
