using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        //private readonly IToastNotification toastNotification;
        private readonly BanService banService;
        private readonly IViewModelMapper<UserDto, UserViewModel> userMapper;

        public UsersController(BanService banService, IViewModelMapper<UserDto, UserViewModel> userMapper)
        {
            this.banService = banService;
            this.userMapper = userMapper;
        }

        public async Task<IActionResult> Index()
        {
            var activeUsers = await this.banService.GetAllAsync("active");
            var bannedUsers = await this.banService.GetAllAsync("banned");
            var activeUsersVM = this.userMapper.MapFrom(activeUsers);
            var bannedUsersVM = this.userMapper.MapFrom(bannedUsers);

            var allUsersVM = new UsersViewModel
            {
                ActiveUsers = activeUsersVM,
                BannedUsers = bannedUsersVM
            };

            return View(allUsersVM);
        }

        public IActionResult CreateBan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBan(Guid id, BanViewModel entity)
        {
            if (ModelState.IsValid)
            {
                await this.banService.CreateAsync(id, entity.Description, entity.Period);
                return RedirectToAction("Index", "Users");
            }
            ModelState.AddModelError(String.Empty, "Please fill in the form!");
            return View(entity);
        }
    }
}