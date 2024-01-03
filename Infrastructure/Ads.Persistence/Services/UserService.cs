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
        var users = await _userManager.Users
            .Include(u => u.Role) // Kullanıcıların rollerini yüklemek için.
            .Include(u => u.Setting) // Kullanıcıların ayarlarını yüklemek için.
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


    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userManager.Users
            .Include(u => u.Role)
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

        var userDto = _mapper.Map<UserDto>(user);

        // Otomatik map edilmiş varlıklarınız varsa, ekstra manuel dönüşüm yapmanız gerekmez.
        // AutoMapper bu dönüşümü sizin için yapacaktır.
        // Eğer özel dönüşümler gerekiyorsa, burada ekleyebilirsiniz.

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
            BirthDate = userDto.BirthDate, 
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
            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            if (createdUser != null)
            {
                const string userRoleName = "User";

                // Eğer rol yoksa oluştur
                if (!await _roleManager.RoleExistsAsync(userRoleName))
                {
                    var roleResult = await _roleManager.CreateAsync(new AppRole { Name = userRoleName });
                    if (!roleResult.Succeeded)
                    {
                        // Rol oluşturma başarısız oldu, hata döndür
                        return IdentityResult.Failed(new IdentityError
                        {
                            Description = $"Could not create the '{userRoleName}' role."
                        });
                    }
                }

                // Rol atamasını yap
                var roleAssignmentResult = await _userManager.AddToRoleAsync(createdUser, userRoleName);
                if (!roleAssignmentResult.Succeeded)
                {
                    // Rol atama işlemi başarısız oldu, hata döndür
                    return IdentityResult.Failed(roleAssignmentResult.Errors.ToArray());
                }
            }
            else
            {
                // Kullanıcı oluşturuldu ama bulunamadı, hata döndür
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "User created but not found."
                });
            }
        }

        // Diğer hataları döndür
        return result;
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


}