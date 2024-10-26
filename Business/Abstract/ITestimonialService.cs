using Core.Helper.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ITestimonialService
	{
        IResult Add(Testimonial testimonial);
        IResult Update(Testimonial testimonial);
        IResult Delete(int id);
        IDataResult<Testimonial> GetById(int id);
        IDataResult<List<Testimonial>> GetAll();
    }
}
