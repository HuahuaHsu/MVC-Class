using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Services;
using EStoreFrontEnd.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EStoreFrontEnd.Controllers
{
	public class MembersController : Controller
	{
		private readonly MemberService _service;

		public MembersController(MemberService service)
		{
			_service = service;
		}

		[Authorize]// This action requires the user to be authenticated
		public IActionResult Index()
		{
			return View();
		}

		[Authorize]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var account = User.Identity.Name;//登入者的帳號
			var member = await _service.GetByAccount(account);//從repo先呼叫GetMemberByAccountAsync，再從service呼叫GetByAccount

			//todo 判斷 member 是否為 null
			var dto = new ChangePasswordDto
			{
				Id = member.Id,
				OrigPassword = model.OrigPassword,
				NewPassword = model.NewPassword
			};

			//方法二 : 從 User.Claims 取出登入者的 Id，並轉換為 int
			//memberId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

			var result = await _service.ChangePasswordAsync(dto);
			if (result.IsSuccess)
			{
				TempData["Message"] = "密碼修改成功";
				return RedirectToAction(nameof(Index));//修改成功後導回會員中心
			}

			ModelState.AddModelError(string.Empty, result.ErrorMessage);
			return View(model);//修改失敗，顯示錯誤訊息並停留在修改密碼頁面

		}
	}
}
