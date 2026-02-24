using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication3.Models.EfModels;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
	public class UsersController : Controller
	{
		private readonly ISpanDemoContext _context;

		public UsersController(ISpanDemoContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			//取得User列表
			var users = _context.Users
						.Select(u => new UserItemViewModel { Id = u.Id, UserName = u.UserName })
						.ToList();

			return View(users);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(UserCreateViewModel vm )
		{
			if (ModelState.IsValid)
			{
				var user = new User{UserName = vm.UserName};
				_context.Users.Add(user);
				_context.SaveChanges();
				await _context.SaveChangesAsync();

				//TempData["msg"] = "已新增資料";

				return RedirectToAction(nameof(Index));
			}
			
			return View(vm);
		}

	}
}

