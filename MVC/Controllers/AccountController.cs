using System.Linq;
using System.Threading.Tasks;
using CookieReaders.Models;
using CookieReaders.Providers;
using CookieReaders.Providers.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieReaders.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IUserRepository _userRepository;

        public AccountController()
        {
            _userManager = new UserManager();
            _userRepository = new UserRepository();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(this.User.Claims.ToDictionary(x => x.Type, x => x.Value));
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userRepository.Validate(model);

            if (user == null) return View(model);

            await _userManager.SignIn(this.HttpContext, user, false);

            return LocalRedirect("~/AnswerJournal/Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVm model)
        {

            if (!ModelState.IsValid)
            {   
                return View("Register", model);
            }
            try
            {
                var user = _userRepository.Register(model);
                 _userManager.SignIn(this.HttpContext, user, false);
            }
            catch(Exception ex) 
            {
                model.Error = ex.GetBaseException().Message;
                return View("Register", model);
            }
            return LocalRedirect("~/Home/Index");
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await _userManager.SignOut(this.HttpContext);
            return RedirectPermanent("~/AnswerJournal/Index");
        }
    }
}