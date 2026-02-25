using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class UploadFile02ApiController : ControllerBase
	{
		private readonly IWebHostEnvironment _env;
		private readonly string _uploads;
		public UploadFile02ApiController(IWebHostEnvironment env)
		{
			_env = env;
			_uploads = Path.Combine(_env.WebRootPath, "uploads");
		}

		[HttpPost]
		[RequestSizeLimit(10 * 1024 * 1024)] // 限制最大上傳 10MB
		public async Task<IActionResult> UploadFile([FromForm] IFormFile f)
		{
			if (f == null || f.Length == 0)
				return BadRequest("請選擇要上傳的檔案。");

			if (!Directory.Exists(_uploads))
				Directory.CreateDirectory(_uploads);

			var filePath = Path.Combine(_uploads, Path.GetFileName(f.FileName));

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await f.CopyToAsync(stream);
			}

			return Ok(new
			{
				f.FileName,
				f.Length,
				FilePath = $"/uploads/{f.FileName}"
			});
		}
	
	}
}
