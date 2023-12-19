﻿using Ads.Application.DTOs.CategoryAdvert;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Application.FluentValidation
{
	public class CategoryAdvertDtoValidator : AbstractValidator<CategoryAdvertDto>
	{
		public CategoryAdvertDtoValidator()
		{
			RuleFor(dto => dto.CategoryId)
				.NotEmpty().WithMessage("Category ID is required.")
				.GreaterThan(0).WithMessage("Category ID must be greater than zero.");

			RuleFor(dto => dto.AdvertId)
				.NotEmpty().WithMessage("Advert ID is required.")
				.GreaterThan(0).WithMessage("Advert ID must be greater than zero.");			
		}
	}
}
