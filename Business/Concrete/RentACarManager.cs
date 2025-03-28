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
    public class RentACarManager(IRentACarDal rentACarDal) : IRentACarService
    {
        private readonly IRentACarDal _rentACarDal = rentACarDal;
        public IDataResult<List<CarGetByFilterDto>> GetByFilter(int locationId, bool available)
        {
            var result = _rentACarDal.GetByFilter(locationId, available);
            if (result != null)
            {
                return new SuccessDataResult<List<CarGetByFilterDto>>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<List<CarGetByFilterDto>>(result, "Not found!");
            }
        }
    }
}
