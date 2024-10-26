using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class FeatureManager(IFeatureDal featureDal) : IFeatureService
	{
		private readonly IFeatureDal _featureDal = featureDal;
		public IResult Add(Feature Feature)
		{
			_featureDal.Add(Feature);
			return new SuccessResult("Successfully added!");
		}

		public IResult Delete(int id)
		{
			var result = _featureDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				result.IsDelete = true;
				_featureDal.Delete(result);
				return new SuccessResult("Deleted successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}

		public IDataResult<List<Feature>> GetAll()
		{
			var result = _featureDal.GetAll(p => p.IsDelete == false).ToList();
			if (result.Count > 0)
			{
				return new SuccessDataResult<List<Feature>>(result, "Got Successfully!");
			}
			else
			{
				return new ErrorDataResult<List<Feature>>(result, "Not found!");
			}
		}

		public IDataResult<Feature> GetById(int id)
		{
			var result = _featureDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Feature>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Feature>(result, "Not found!");
			}
		}

		public IResult Update(Feature feature)
		{
			var result = _featureDal.Get(p => p.Id == feature.Id && p.IsDelete == false);
			if (result != null)
			{
				result.Name = feature.Name;
				_featureDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
