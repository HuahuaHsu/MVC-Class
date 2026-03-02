using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Services;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EStoreFrontEnd.Controllers
{
	public class AuthController : Controller
	{
		private readonly AuthService _authService;

		public AuthController(AuthService authService)
		{
			_authService = authService;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(RegisterViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			//call AuthService to register user
			var result = _authService.Register(vm.ToDto());

			if (!result.IsSuccess)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

			return View("RegisterConfirm");
		}

		public IActionResult ActiveRegister(int memberId, string confirmCode)
		{
			// Call AuthService to activate the user
			try
			{
				_authService.ActiveRegister(memberId, confirmCode);
			}
			catch (Exception ex)
			{

			}
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
		{
			if (!ModelState.IsValid) return View(vm);

			var dto = new LoginDto
			{
				Account = vm.Account,
				Password = vm.Password
			};
			var result = await _authService.LoginAsync(dto);

			if (!result.IsSuccess)
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

			await ProcessLogin(vm.Account);//建立 ClaimsIdentity 並簽發 Cookie

			//登入成功後，重定向到 returnUrl 或首頁
			return LocalRedirect(returnUrl);
		}

		private async Task ProcessLogin(string account)
		{
			//1.根據帳號取得會員資料
			//var member = await _authService.GetMemberByAccountAsync(account);
			//2.如果會員資料不存在，則回傳登入失敗

			//3.如果會員資料存在，則建立 ClaimsIdentity 並簽發 Cookie
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name,account)
				//這裡可以根據需要添加其他的 Claim，例如角色、權限等
			};
			//4.簽發 Cookie
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//這裡的 CookieAuthenticationDefaults.AuthenticationScheme 是指使用預設的 Cookie 認證方案=Cookie，你也可以自定義一個方案名稱

			var principal = new ClaimsPrincipal(identity);

			//這裡的 AuthenticationProperties 可以用來設定 Cookie 的屬性，例如是否持久化、過期時間等
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
			{
				IsPersistent = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
			});
		}


		public async Task<IActionResult> Logout()
		{
			//1.清除 Cookie
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			//2.重定向到首頁
			return RedirectToAction("Index", "Home");
		}
	}
}
