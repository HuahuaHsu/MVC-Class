using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
	public class Uploadfile01Controller : Controller
	{
		public IActionResult Create(IFormFile f)
		{
			return View();
		}
	}
}
