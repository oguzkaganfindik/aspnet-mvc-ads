﻿using Ads.Application.DTOs.AdvertComment;
using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using AutoMapper;
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
            .Include(u => u.AdvertComments) // Kullanıcının yorumlarını yüklemek için eklendi.
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;

        var userDto = _mapper.Map<UserDto>(user);
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Any())
        {
            var role = await _roleManager.FindByNameAsync(roles.First());
            userDto.Role = _mapper.Map<RoleDto>(role);
        }

        // Kullanıcının yorumlarını DTO'ya dönüştür.
        if (user.AdvertComments != null)
        {
            userDto.AdvertComments = _mapper.Map<ICollection<AdvertCommentDto>>(user.AdvertComments);
        }

        return userDto;
    }


}