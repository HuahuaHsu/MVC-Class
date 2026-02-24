using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
	public class UserItemViewModel
	{
		public int Id { get; set; }

		[Display(Name ="使用者帳號")]
		public string UserName { get; set; }
	}

	public class UserCreateViewModel
	{
		[Display(Name = "使用者帳號")]
		[Required(ErrorMessage ="{0}必填")]
		public string UserName { get; set; }
	}

	public class UserUpdateViewModel
	{
		public int Id { get; set; }

		[Display(Name = "使用者帳號")]
		[Required(ErrorMessage = "{0}必填")]
		public string UserName { get; set; }
	}


}
