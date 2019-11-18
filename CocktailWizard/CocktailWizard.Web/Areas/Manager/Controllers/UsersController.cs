using CocktailWizard.Data.DtoEntities;
using CocktailWizard.Services.Contracts;
using CocktailWizard.Web.Areas.Manager.Models;
using CocktailWizard.Web.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Threading.Tasks;

namespace CocktailWizard.Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        //private readonly IToastNotification toastNotification;
        private readonly IBanService banService;
        private readonly IViewModelMapper<UserDto, UserViewModel> userMapper;
        private readonly IToastNotification toastNotification;

        public UsersController(IBanService banService, 
                               IViewModelMapper<UserDto, UserViewModel> userMapper, 
                               IToastNotification toastNotification)
        {
            this.banService = banService ?? throw new ArgumentNullException(nameof(banService));
            this.userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
            this.toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
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
                this.toastNotification.AddSuccessToastMessage("User successfully banned");
                return RedirectToAction("Index", "Users");
            }
            ModelState.AddModelError(String.Empty, "Please fill in the form!");
            this.toastNotification.AddSuccessToastMessage("User couldn't be banned");
            return View(entity);
        }
    }
}