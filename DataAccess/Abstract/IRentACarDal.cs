using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRentACarDal : IBaseReporsitory<RentACar>
    {
        List<CarGetByFilterDto> GetByFilter(int locationId, bool available);
    }
}
