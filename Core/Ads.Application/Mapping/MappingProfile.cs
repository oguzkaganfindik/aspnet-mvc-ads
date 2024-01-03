using Ads.Application.DTOs.Advert;
using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.AdvertImage;
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

namespace Ads.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdvertDto, Advert>().ReverseMap();
            CreateMap<AdvertCommentDto, AdvertComment>().ReverseMap();
            CreateMap<AdvertImageDto, AdvertImage>().ReverseMap();
            CreateMap<AdvertRatingDto, AdvertRating>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
            CreateMap<SubCategoryAdvertDto, SubCategoryAdvert>().ReverseMap();
            CreateMap<CategoryAdvertDto, CategoryAdvert>().ReverseMap();
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.Setting, opt => opt.MapFrom(src => src.Setting))
                .ReverseMap()
                .ForMember(dest => dest.Setting, opt => opt.MapFrom(src => src.Setting));
            CreateMap<SettingDto, Setting>().ReverseMap();
            CreateMap<RoleDto, AppRole>().ReverseMap();
            CreateMap<UserDto, AppUser>().ReverseMap();
            CreateMap<RegisterDto, AppUser>().ReverseMap();
            CreateMap<LoginDto, AppUser>().ReverseMap();
            CreateMap<ForgotPasswordDto, AppUser>().ReverseMap();
            CreateMap<ResetPasswordDto, AppUser>().ReverseMap();


        }

    }
}
