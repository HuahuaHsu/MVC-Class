using System.ComponentModel.DataAnnotations;

namespace EStoreFrontEnd.Models.ViewModels
{
	public class ForgetPasswordVM
	{
		[Display(Name = "帳號")]
		[Required(ErrorMessage = "{0}必填")]
		[StringLength(30)]
		public string Account { get; set; }

		[Display(Name = "電子郵件")]
		[Required(ErrorMessage = "{0}必填")]
		[StringLength(256)]
		public string Email { get; set; }
	}
}
