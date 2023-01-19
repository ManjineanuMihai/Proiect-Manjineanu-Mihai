using Microsoft.AspNetCore.Mvc;
using MPD_Manjineanu_Mihai.Models;
using MPD_Manjineanu_Mihai.Repositories;

namespace MPD_Manjineanu_Mihai.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IUserRepository repo, 
            ILogger<AuthenticationController> logger,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _repo = repo;
           _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult RegisterIndex()
        {
            return View("RegisterView");
        }
        public IActionResult LoginIndex()
        {
            return View("LoginView");
        }
        public IActionResult UpdateIndex()
        {
            return View("UpdateUserView");
        }
        public IActionResult Register(UserModel user)
        {
            try
            {
                _repo.RegisterUser(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("LoginIndex");
        }      
        public IActionResult Login(UserModel user)
        {
            if(_repo.LoginUser(user.Username, user.Password))
            {
                _httpContextAccessor.HttpContext.Session.SetString("username", user.Username);

                return RedirectToAction("Index", "Activity");
            }
            else
            {
                return RedirectToAction("LoginIndex");
            }           
        }
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("username");
            return RedirectToAction("LoginIndex");
        }
        public async Task<IActionResult> DeleteUser()
        {
            var user = _httpContextAccessor.HttpContext.Session.GetString("username");
           await  _repo.DeleteUser(user);
            return RedirectToAction("Logout");
        }
        public async Task<IActionResult> UpdateUser(string password)
        {
            var user = _httpContextAccessor.HttpContext.Session.GetString("username");
            var dbUser = await _repo.GetUser(user);

            if(dbUser == null) RedirectToAction("LoginIndex");
         
            try
            {
                dbUser.Password = password;
                await _repo.Update(dbUser);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("LogOut");
        }
    }
}
