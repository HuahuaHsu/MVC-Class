using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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


		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			var vm = new UserUpdateViewModel { Id = user.Id, UserName = user.UserName };
			return View(vm);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,UserUpdateViewModel vm)
		{
			if (id != vm.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var user = new User{ Id = vm.Id, UserName = vm.UserName };
				_context.Update(user);
				 _context.SaveChangesAsync();
				
				//TempData["msg"] = "資料已更新";
				return RedirectToAction(nameof(Index));
			
			}
			return View(vm);
		}


	}
}

