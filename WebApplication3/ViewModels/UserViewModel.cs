using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModels
{
	public class UserItemViewModel //index 顯示用
	{
		public int Id { get; set; }

		[Display(Name="使用者帳號")] // 顯示名稱
		public string UserName { get; set; }
	}

	public class UserCreateViewModel 
	{

		[Display(Name = "使用者名稱")]
		[Required(ErrorMessage = "{0}必填")] //{0}會被替換成 Display 的 Name
		
		//public int Id { get; set; } // 創建不需要 Id，因為它是由資料庫自動生成的
		public string UserName { get; set; }
	}

	public class UserUpdateViewModel //
	{
		public int Id { get; set; }

		[Display(Name = "使用者名稱")]
		[Required(ErrorMessage = "{0}必填")]
		public string UserName { get; set; }
	}
}
