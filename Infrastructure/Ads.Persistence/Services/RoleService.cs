using Ads.Application.DTOs.Role;
using Ads.Application.DTOs.User;
using Ads.Application.Services;
using Ads.Domain.Entities.Concrete;
using Ads.Persistence.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public RoleService(RoleManager<AppRole> roleManager, AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
    public async Task<RoleDto> GetRoleWithUsersAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role == null)
        {
            return null; 
        }

        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
        var roleDto = _mapper.Map<RoleDto>(role);
        roleDto.Users = _mapper.Map<ICollection<UserDto>>(usersInRole);

        return roleDto;
    }
}
