using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication3.Controllers
{
	public class Uploadfile01Controller : Controller
	{
		private readonly IWebHostEnvironment _env;
		private readonly string _uploads;

		public Uploadfile01Controller(IWebHostEnvironment env)
		{
			_env = env;//網站實體路徑
			_uploads = Path.Combine(_env.WebRootPath, "uploads");
		}

		//方法一:使用if else巢狀式
		//public IActionResult Create(IFormFile f)
		//{
		//	var allowExts = new string[] { ".jpg", ".png", ".jpeg" };
		//	if (f == null || f.Length == 0)
		//		ModelState.AddModelError("", "必須上傳檔案");
		//	else
		//	{
		//		string ext = Path.GetExtension(f.FileName); //抓路徑的副檔名".jpg"
		//		if (allowExts.Contains(ext) == false)
		//			ModelState.AddModelError("", "副檔名不合規定");
		//		else
		//		{
		//			string newFileName = Guid.NewGuid().ToString("N")+ext;//"N"(較短)產生隨機不包含-

		//			var filePath = Path.Combine(_uploads, newFileName);//完整路徑(環境路徑+檔名)

		//			using(var stream = new FileStream(filePath, FileMode.Create))
		//			{
		//				f.CopyTo(stream);
		//			}
		//		}
		//	}
		//	return View();
		//}


		//方法二:使用try catch呼叫檔名判斷方法，檔名判斷另外寫一個方法
		public IActionResult Create(IFormFile f)
		{
			try
			{
				var newFileName = UploadProductImage(f);
			}
			catch(Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return View();
		}

		private string UploadProductImage(IFormFile f)
		{
			var allowExts = new string[] { ".jpg", ".png", ".jpeg" };

			if (f == null || f.Length == 0)
				throw new Exception("必須上傳檔案");

			string ext = Path.GetExtension(f.FileName); //抓路徑的副檔名".jpg"
			if (allowExts.Contains(ext) == false)
				throw new Exception("副檔名不合規定");

			string newFileName = Guid.NewGuid().ToString("N") + ext;//"N"(較短)產生隨機不包含-

			var filePath = Path.Combine(_uploads, newFileName);//完整路徑(環境路徑+檔名)

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				f.CopyTo(stream);
			}

			return newFileName;
		}
	}
}
