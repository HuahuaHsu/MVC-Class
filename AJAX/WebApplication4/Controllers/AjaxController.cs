using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
	public class AjaxController : Controller
	{
		private readonly NorthwindContext _context;

		public AjaxController(NorthwindContext context)
		{
			_context = context;
		}

		// GET: /Ajax/Greet
		[HttpGet]
		public string Greet(string name)
		{
			Thread.Sleep(3000); // Simulate a delay
			return $"Hello, {name}!";
		}

		// POST: /Ajax/PostGreet
		[HttpPost]
		public string PostGreet(string name)
		{
			Thread.Sleep(3000);
			return $"Hello, {name}!";
		}

		// POST: /Ajax/CheckName
		[HttpPost]
		public string CheckName(string firstName)
		{
			bool exists = _context.Employees.Any(e => e.FirstName == firstName);
			return exists ? "true" : "false";
		}
	}
}
