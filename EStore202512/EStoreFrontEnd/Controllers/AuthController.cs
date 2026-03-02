using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Services;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

			if(!result.IsSuccess)
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

	}
}
