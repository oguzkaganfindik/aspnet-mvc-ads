using Ads.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Web.Mvc.Areas.Admin.Controllers
{
    //[Area("Admin"), Authorize(Policy = "UserPolicy")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: RolesController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _roleService.GetAllRolesAsync();
            return View(model);
        }

        // GET: RolesController/Details/5
        public async Task<IActionResult> DetailAsync(int id)
        {
            var roleDto = await _roleService.GetRoleWithUsersAsync(id);
            if (roleDto == null)
            {
                return NotFound();
            }
            return View(roleDto);
        }
    }
}