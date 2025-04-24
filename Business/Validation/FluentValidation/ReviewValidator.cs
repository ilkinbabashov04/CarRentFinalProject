using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
	public class ReviewValidator : AbstractValidator<Review>
	{
        public ReviewValidator()
        {
            RuleFor(x=>x.CustomerName).NotEmpty().MinimumLength(3).WithMessage("The length of name must be more than 3").NotNull().WithMessage("Name can not be send as empty");
            RuleFor(x => x.RatingValue).NotEmpty().WithMessage("It will not be empty!");
			RuleFor(x => x.Comment).NotEmpty().MinimumLength(10).WithMessage("The length of comment must be more than 10").NotNull().WithMessage("Name can not be send as empty").MaximumLength(500).WithMessage("The length of name will not be more than 500 charachter");
		}
	}
}
