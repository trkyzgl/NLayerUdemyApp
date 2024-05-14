using FluentValidation;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {

        public ProductDtoValidator()
        {
            RuleFor(x=>x.Name).NotNull().WithMessage("{PropertyName} is Required").NotEmpty().WithMessage("{PropertyName} is Required");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{Price} must be greader 0"); 
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{Stock} must be greader 0");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{CategoryId} must be greader 0");
        }
    }
}
