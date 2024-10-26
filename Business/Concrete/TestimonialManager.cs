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
	public class TestimonialManager(ITestimonialDal testimonialDal) : ITestimonialService
	{
        private readonly ITestimonialDal _testimonialDal = testimonialDal;
        public IResult Add(Testimonial testimonial)
        {
            _testimonialDal.Add(testimonial);
            return new SuccessResult("Successfully added!");
        }

        public IResult Delete(int id)
        {
            var result = _testimonialDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                result.IsDelete = true;
                _testimonialDal.Delete(result);
                return new SuccessResult("Deleted successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }

        public IDataResult<List<Testimonial>> GetAll()
        {
            var result = _testimonialDal.GetAll(p => p.IsDelete == false).ToList();
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Testimonial>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<Testimonial>>(result, "Not found!");
            }
        }

        public IDataResult<Testimonial> GetById(int id)
        {
            var result = _testimonialDal.Get(p => p.Id == id && p.IsDelete == false);
            if (result != null)
            {
                return new SuccessDataResult<Testimonial>(result, "Got Successfully");
            }
            else
            {
                return new ErrorDataResult<Testimonial>(result, "Not found!");
            }
        }

        public IResult Update(Testimonial testimonial)
        {
            var result = _testimonialDal.Get(p => p.Id == testimonial.Id && p.IsDelete == false);
            if (result != null)
            {
                result.Title = testimonial.Title;
                result.Comment = testimonial.Comment;
                result.Name = testimonial.Name;
                result.ImageUrl = testimonial.ImageUrl;
                _testimonialDal.Update(result);
                return new SuccessResult("Updated successfully!");
            }
            else
            {
                return new ErrorResult("Not found!");
            }
        }
    }
}
