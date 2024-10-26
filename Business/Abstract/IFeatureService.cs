using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IFeatureService
	{
		IResult Add(Feature feature);
		IResult Update(Feature feature);
		IResult Delete(int id);
		IDataResult<Feature> GetById(int id);
		IDataResult<List<Feature>> GetAll();
	}
}
