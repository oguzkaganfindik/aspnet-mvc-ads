using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.Advertmage;
using Ads.Application.DTOs.AdvertRating;
using Ads.Application.DTOs.Category;
using Ads.Application.DTOs.CategoryAdvert;
using Ads.Application.DTOs.Page;
using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.Setting;
using Ads.Application.DTOs.SubCategory;
using Ads.Application.DTOs.SubCategoryAdvert;
using Ads.Application.DTOs.User;
using Ads.Domain.Entities.Concrete;
using AutoMapper;

namespace Ads.Application.MappingProfile
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Advert, AdvertDto>().ReverseMap();
			CreateMap<AdvertComment, AdvertCommentDto>().ReverseMap();
			CreateMap<AdvertImage, AdvertImageDto>().ReverseMap();
			CreateMap<AdvertRating, AdvertRatingDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
			CreateMap<SubCategoryAdvert, SubCategoryAdvertDto>().ReverseMap();
			CreateMap<CategoryAdvert, CategoryAdvertDto>().ReverseMap();
			CreateMap<Page, PageDto>().ReverseMap();
			CreateMap<Setting, SettingDto>().ReverseMap();
			CreateMap<Role, RoleDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
			
		}

	}
}
