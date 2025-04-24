using Business.Abstract;
using Business.Validation.FluentValidation;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ReviewManager(IReviewDal reviewDal) : IReviewService
	{
		private readonly IReviewDal _reviewDal = reviewDal;

		public IResult Add(Review review)
		{
			ReviewValidator validator = new ReviewValidator();
			var validatorResult = validator.Validate(review);
			if (!validatorResult.IsValid)
			{
				return new ErrorResult(validatorResult.Errors.FirstOrDefault()?.ErrorMessage);
			}
			_reviewDal.Add(review);
			return new SuccessResult("Successfully added!");
		}

		public IDataResult<Review> GetById(int id)
		{
			var result = _reviewDal.Get(p => p.Id == id && p.IsDelete == false);
			if (result != null)
			{
				return new SuccessDataResult<Review>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<Review>(result, "Not found!");
			}
		}

		public IDataResult<List<Review>> GetReviewsByCarId(int id)
		{
			var result = _reviewDal.GetReviewsByCarId(id);
			if (result != null)
			{
				return new SuccessDataResult<List<Review>>(result, "Got Successfully");
			}
			else
			{
				return new ErrorDataResult<List<Review>>(result, "Not found!");
			}
		}

		public IResult Update(Review review)
		{
			var result = _reviewDal.Get(p => p.Id == review.Id && p.IsDelete == false);
			if (result != null)
			{
				result.CustomerName = review.CustomerName;
				result.CustomerImage = review.CustomerImage;
				result.Comment = review.Comment;
				result.RatingValue = review.RatingValue;
				_reviewDal.Update(result);
				return new SuccessResult("Updated successfully!");
			}
			else
			{
				return new ErrorResult("Not found!");
			}
		}
	}
}
