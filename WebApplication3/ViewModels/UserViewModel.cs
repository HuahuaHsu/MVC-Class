using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
	public class UserItemViewModel
	{
		public int Id { get; set; }

		[Display(Name="使用者帳號")]
		public string UserName { get; set; }
	}

	public class UserCreateViewModel
	{

		[Display(Name = "使用者帳號")]
		[Required]
		//public int Id { get; set; } // 創建不需要 Id，因為它是由資料庫自動生成的
		public string UserName { get; set; }
	}

	public class UserUpdateViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
	}
}
