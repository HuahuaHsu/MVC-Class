using System.ComponentModel.DataAnnotations;

namespace EStoreFrontEnd.Models.ViewModels
{
	public class ChangePasswordViewModel
	{

		[Required(ErrorMessage = "{0}為必填")]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "{0}長度必須在{1}到{2}個字元之間")]
		[DataType(DataType.Password)]
		[Display(Name = "原始密碼")]
		public string OrigPassword { get; set; }


		[Required(ErrorMessage = "{0}為必填")]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "{0}長度必須在{1}到{2}個字元之間")]
		[DataType(DataType.Password)]
		[Display(Name = "新的密碼")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "{0}為必填")]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "{0}長度必須在{1}到{2}個字元之間")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "新的密碼和確認密碼不符")]
		[Display(Name = "確認密碼")]
		public string ConfirmPassword { get; set; }
	}
}
