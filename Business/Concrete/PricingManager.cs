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
	public class PricingManager(IPricingDal pricingDal) : IPricingService
	{
		private readonly IPricingDal _pricingDal = pricingDal;
		public IResult Add(Pricing pricing)
		{
			_pricingDal.Add(pricing);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _pricingDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_pricingDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Pricing>> GetAll()
		{
			var result = _pricingDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Pricing>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Pricing>>(result, "Not found!");
			}
		}

		public IDataResult<Pricing> GetById(int id)
		{
			var result = _pricingDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Pricing>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Pricing>(result, "Not found!");
			}
		}

        public IDataResult<List<GetPricingByCarIdDto>> GetPricingByCarId(int id)
        {
			var result = _pricingDal.GetPricingByCarId(id);
            if (result != null)
            {
                return new SuccessDataResult<List<GetPricingByCarIdDto>>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<List<GetPricingByCarIdDto>>(result, "Not found!");
            }
        }

        public IResult Update(Pricing pricing)
		{
			var result = _pricingDal.Get(p => p.Id == pricing.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = pricing.Name;
				_pricingDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
