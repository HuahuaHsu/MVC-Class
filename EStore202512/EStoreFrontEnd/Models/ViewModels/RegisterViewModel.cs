using EStoreFrontEnd.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EStoreFrontEnd.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "帳號為必填")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "帳號長度不得超過30個字")]
		[Display(Name="帳號")]
		public string Account { get; set; }

		[Required(ErrorMessage = "密碼為必填")]
		[DataType(DataType.Password)]
		[StringLength(70, MinimumLength = 6, ErrorMessage = "密碼長度必須在6到70個字元之間")]
		[Display(Name = "密碼")]
		public string Password { get; set; }

		[Required(ErrorMessage = "確認密碼為必填")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "密碼和確認密碼不符")]
		[Display(Name = "確認密碼")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "電子郵件為必填")]
		[StringLength(256, MinimumLength = 1, ErrorMessage = "電子郵件長度不得超過256個字")]
		[EmailAddress(ErrorMessage = "電子郵件格式不正確")]
		[Display(Name = "電子郵件")]
		public string Email { get; set; }

		[Required(ErrorMessage = "姓名為必填")]
		[StringLength(30, MinimumLength = 1, ErrorMessage = "姓名長度不得超過30個字")]
		[Display(Name = "姓名")]
		public string Name { get; set; }

		[StringLength(10, ErrorMessage = "手機號碼長度不得超過10個字")]
		[RegularExpression(@"^09\d{8}$", ErrorMessage = "手機號碼格式不正確(09開頭的10碼數字)")]
		[Display(Name = "手機號碼")]
		public string Mobile { get; set; }
	}

	public static class RegisterViewModelExtension
	{
		public static RegisterDto ToRegisterDto(this RegisterViewModel viewModel)
		{
			return new RegisterDto
			{
				Account = viewModel.Account,
				Password = viewModel.Password,
				Email = viewModel.Email,
				Name = viewModel.Name,
				Mobile = viewModel.Mobile
			};
		}
	}
}
