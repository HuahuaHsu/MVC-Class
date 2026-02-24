using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
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

		public IActionResult Index()
		{
			//取得users
			var users = _context.Users
				.Select(u => new UserItemViewModel { Id = u.Id, UserName = u.UserName})
				.ToList();

			return View(users);
		}
	}
}
