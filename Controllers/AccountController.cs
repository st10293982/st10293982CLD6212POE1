////using Microsoft.AspNetCore.Authentication;
////using Microsoft.AspNetCore.Authentication.Cookies;
////using Microsoft.AspNetCore.Mvc;
////using System.Security.Claims;
////using st10293982CLD6212POE1.Models;
////using Azure.Storage.Blobs.Models;
////using st10293982CLD6212POE1.Services;

////namespace st10293982CLD6212POE1.Controllers
////{
////    public class AccountController : Controller
////    {
////        private readonly IAuthService _authService;

////        public AccountController(IAuthService authService)
////        {
////            _authService = authService;
////        }

////        //get /account/register
////        [HttpGet]
////        public IActionResult Register()
////        {
////            return View();
////        }

////        [HttpPost]
////        public async Task<IActionResult> Register(RegisterViewModel model)
////        {
////            if (ModelState.IsValid)
////            {
////                bool success = await _authService.RegisterAsync(model.Email, model.Password, model.FullName);
////                if (success)

////                    return RedirectToAction("Login");
////                ModelState.AddModelError("", "User already exists.");
////            }
////            return View(model);
////        }

////        [HttpGet]
////        public IActionResult Login()
////        {
////            return View();
////        }

////        [HttpPost]
////        public async Task<IActionResult> Login(LoginViewModel model)
////        {
////            if (ModelState.IsValid)
////            {
////                var user = await _authService.LoginAsync(model.Email, model.Password);
////                if (user != null)
////                {

////                    var claims = new List<Claim>
////                {
////                    new Claim(ClaimTypes.Name, user.FullName),
////                    new Claim(ClaimTypes.Email, user.Email)
////                };
////                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
////                    var authProperties = new AuthenticationProperties
////                    {
////                        IsPersistent = model.RememberMe
////                    };

////                    await HttpContent.SignInAsync(
////                        CookieAuthenticationDefaults.AuthenticationScheme,
////                        new ClaimsPrincipal(claimsIdentity),
////                        authProperties);
////                    return RedirectToAction("Index", "Home");

////                }
////                ModelState.AddModelError("", "Invalid login attempt.");
////            }
////            return View(model);
////        }

////        [HttpPost]
////        public async Task<IActionResult> Logout()
////        {
////            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
////            return RedirectToAction("Login", "Account");
////        }
////    }
////    }

//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using st10293982CLD6212POE1.Models;
//using st10293982CLD6212POE1.Services;

//namespace st10293982CLD6212POE1.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IAuthService _authService;

//        public AccountController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                bool success = await _authService.RegisterAsync(model.Email, model.Password, model.FullName);
//                if (success)
//                    return RedirectToAction("Login");

//                ModelState.AddModelError("", "User already exists.");
//            }

//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _authService.LoginAsync(model.Email, model.Password);
//                if (user != null)
//                {
//                    var claims = new List<Claim>
//                    {
//                        new Claim(ClaimTypes.Name, user.FullName),
//                        new Claim(ClaimTypes.Email, user.Email)
//                    };
//                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                    var authProperties = new AuthenticationProperties
//                    {
//                        IsPersistent = model.RememberMe
//                    };

//                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

//                    return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError("", "Invalid login attempt.");
//            }

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//            return RedirectToAction("Login", "Account");
//        }
//    }
//}

