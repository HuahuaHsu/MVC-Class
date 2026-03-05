using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;
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
		private readonly EStoreContext _context;

		public MembersController(MemberService service,EStoreContext context)
		{
			_service = service;
			_context = context;
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

		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ForgetPassword(ForgetPasswordVM model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}
			//實作忘記密碼的功能
			//1.根據使用者輸入的帳號和email，去資料庫確認是否有這個會員
			var member = _context.Members
				.Where(m => m.Account == model.Account && m.Email == model.Email)
				.FirstOrDefault();

			if (member == null)
			{
				ModelState.AddModelError(string.Empty, "查無此會員");
				return View(model);
			}

			//1.5 更新 Member 的 ResetPasswordConfirmCode 欄位，並存回資料庫
			member.ResetPasswordConfirmCode = Guid.NewGuid().ToString();
			_context.SaveChanges();

			//2.計算出完整url
			var urlTemplate = Request.Scheme + "://" + //（http或https）
				Request.Host.Value + "/" +  //（域名和端口號）
				"Members/ResetPassword?memberId={0}&confirmCode={1}";

			var Url = string.Format(urlTemplate, member.Id, member.ResetPasswordConfirmCode);

			//3.寄送email給使用者，裡面包含重設密碼的連結

			return View("ForgetPasswordConfirm");
		}

		public IActionResult ResetPassword(int memberid, string confirmCode)
		{


			return Content($"測試成功！收到 MemberID: {memberid}, Code: {confirmCode}");
		}
	}
}
