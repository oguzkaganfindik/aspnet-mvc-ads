using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;
  

    public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
     
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.Include(x => x.Roles).Include(x => x.Setting)

            .ToListAsync();

        // AutoMapper kullanarak User nesnelerini UserDto'ya dönüştür.
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        // Kullanıcıların rollerini manuel olarak doldur.
        foreach (var userDto in userDtos)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var role = await _roleManager.FindByNameAsync(roles.First());
                userDto.Role = _mapper.Map<RoleDto>(role);
            }
        }

        return userDtos;
    }


    public async Task<TDto> GetUserByIdAsync<TDto>(int id) where TDto: BaseUserDto
    {
        var user = await _userManager.Users
             .Include(u => u.Setting)
            .Include(u => u.AdvertComments)
             .Include(u => u.AdvertRatings)
            .Include(u => u.Adverts)
                    .ThenInclude(a => a.CategoryAdverts)
                        .ThenInclude(ca => ca.Category)
            .Include(u => u.Adverts)
                .ThenInclude(a => a.AdvertRatings)
            .Include(u => u.Adverts)
                .ThenInclude(a => a.AdvertComments)
            .Include(u => u.Adverts)
                .ThenInclude(a => a.AdvertImages)
            .Include(u => u.Adverts)
                .ThenInclude(a => a.SubCategoryAdverts)
                    .ThenInclude(sca => sca.SubCategory)

            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return null;
        }



        var userDto = _mapper.Map<TDto>(user);
        userDto.Roles = await _userManager.GetRolesAsync(user);

        return userDto;
    }


    public async Task<IdentityResult> CreateUserAsync(UserDto userDto, IFormFile? userImageFile)
    {
        var user = new AppUser
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            IsActive = userDto.IsActive,
            PhoneNumber = userDto.Phone,
            Address = userDto.Address,
            UserImagePath = "", // Bu alan aşağıda dosya yüklemesi ile doldurulacak.
            SettingId = 1,
        };

        if (userImageFile != null)
        {
            user.UserImagePath = await FileHelper.FileLoaderAsync(userImageFile, "/Img/UserImages/");
        }

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            // Rol ataması için kullanıcının UserName'ini kullanın
            // Yeni oluşturulan kullanıcı için UserName null değilse bu adımı uygulayın

            // Rol atamasını yap
            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleAssignmentResult.Succeeded)
            {
                // Rol atama işlemi başarısız oldu, hata döndür
                return IdentityResult.Failed(roleAssignmentResult.Errors.ToArray());
            }

        }

        // Diğer hataları döndür
        return result;
    }
    public async Task<IdentityResult> UpdateUserAsync(UserEditDto userDto , IFormFile? file)
    {
        var existingUser = await _userManager.FindByIdAsync(userDto.Id.ToString());
        if (existingUser == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = $"User with ID {userDto.Id} not found."
            });
        }

        
        AppUser? appUser = _mapper.Map(userDto , existingUser);

        appUser.SettingId = userDto.UserTheme ? 1 : 2;

        //Profil resmini güncelle eğer yeni bir dosya yüklendiyse
        if (file != null)
        {
            var imagePath = await FileHelper.FileLoaderAsync(file, "/Img/UserImages/");
            appUser.UserImagePath = imagePath;
        }


        // Kullanıcıyı güncelle
        var result = await _userManager.UpdateAsync(appUser);

        // Rol atamasını kontrol et (eğer rol değişikliği varsa)
        var currentRoles = await _userManager.GetRolesAsync(appUser);
        var selectedRole = appUser.Roles.FirstOrDefault()?.Name;

        if (!string.IsNullOrEmpty(selectedRole) && !currentRoles.Contains(selectedRole))
        {
            // Mevcut rollerden çıkar
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(appUser, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                return removeRolesResult;
            }

            // Yeni role ekle
            var addRoleResult = await _userManager.AddToRoleAsync(appUser, selectedRole);
            if (!addRoleResult.Succeeded)
            {
                return addRoleResult;
            }
        }

        return result;


    }




 }



//public async Task<IdentityResult> EditUserAsync(UserDto userDto, IFormFile? userImageFile)
//{
//    // Öncelikle, düzenlenecek kullanıcıyı bul
//    var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
//    if (user == null)
//    {
//        return IdentityResult.Failed(new IdentityError
//        {
//            Description = $"User with ID {userDto.Id} not found."
//        });
//    }

//    // Kullanıcı bilgilerini güncelle
//    user.Email = userDto.Email;
//    user.UserName = userDto.UserName;
//    user.FirstName = userDto.FirstName;
//    user.LastName = userDto.LastName;
//    user.PhoneNumber = userDto.Phone;
//    user.Address = userDto.Address;
//    user.IsActive = userDto.IsActive;
//    user.BirthDate = userDto.BirthDate;
//    // Diğer alanlarınızı burada güncelleyin...

//    // Profil resmini güncelle eğer yeni bir dosya yüklendiyse
//    if (userImageFile != null)
//    {
//        var imagePath = await FileHelper.FileLoaderAsync(userImageFile, "/Img/UserImages/");
//        user.UserImagePath = imagePath;
//    }

//    // Kullanıcıyı güncelle
//    var result = await _userManager.UpdateAsync(user);

//    // Rol atamasını kontrol et (eğer rol değişikliği varsa)
//    var currentRoles = await _userManager.GetRolesAsync(user);
//    var selectedRole = userDto.Role?.Name;
//    if (!string.IsNullOrEmpty(selectedRole) && !currentRoles.Contains(selectedRole))
//    {
//        // Mevcut rollerden çıkar
//        var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
//        if (!removeRolesResult.Succeeded)
//        {
//            return removeRolesResult;
//        }

//        // Yeni role ekle
//        var addRoleResult = await _userManager.AddToRoleAsync(user, selectedRole);
//        if (!addRoleResult.Succeeded)
//        {
//            return addRoleResult;
//        }
//    }

//    return result;
//}


