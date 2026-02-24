using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.EfModels;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
	public class UsersController : Controller
	{
		private readonly ISpanDemoContext _context;

		public UsersController(ISpanDemoContext context)
		{
			_context = context;
		}
		public IActionResult Index()//顯示使用者列表
		{
			var users = _context.Users
				.Select(u => new UserItemViewModel
				{
					Id = u.Id,
					UserName = u.UserName,
				}).ToList();
		
			return View(users);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(UserCreateViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var user = new User() { UserName = vm.UserName };
				_context.Users.Add(user);
				_context.SaveChanges();

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

			var user = _context.Users.Find(id);
			if (user == null)
			{
				return NotFound();
			}
			var vm = new UserUpdateViewModel { Id=user.Id,UserName=user.UserName };
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, UserUpdateViewModel vm)
		{
			if (id != vm.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var user = new User {Id=vm.Id, UserName = vm.UserName };
				_context.Update(user);
				_context.SaveChangesAsync();

				//TempData["msg"] = "紀錄已更新";
	
				return RedirectToAction(nameof(Index));
			}
			return View(vm);
		}
	}
}
