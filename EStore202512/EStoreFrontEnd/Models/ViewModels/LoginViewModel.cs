using System.ComponentModel.DataAnnotations;

namespace EStoreFrontEnd.Models.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = "{0}為必填")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "{0}長度不得超過{1}個字")]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required(ErrorMessage = "{0}為必填")]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "{0}長度必須在{1}到{2}個字元之間")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
