using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
	public class Uploadfile01Controller : Controller
	{
		public IActionResult Create(IFormFile f)
		{
			var allowExts = new string[] { ".jpg", ".png", "jpeg" };
			if (f == null || f.Length == 0)
				ModelState.AddModelError("", "必須上傳檔案");
			else
			{
				string ext = Path.GetExtension(f.FileName); //抓路徑的副檔名".jpg"
				if (allowExts.Contains(ext) == false)
					ModelState.AddModelError("", "副檔名不合規定");
				else
				{

				}
			}

			return View();
		}
	}
}
