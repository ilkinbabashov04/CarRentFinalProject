using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class SocialMediaValidator : AbstractValidator<SocialMedia>
    {
        public SocialMediaValidator()
        {
            RuleFor(s => s.Name).MinimumLength(3).WithMessage("The length of name must be more than 3").NotNull().WithMessage("Name can not be send as empty");
        }
    }
}
