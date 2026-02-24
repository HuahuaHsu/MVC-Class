using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
	public class UserItemViewModel
	{
		
		
		public int Id { get; set; }

		//dto用來傳輸資料，viewmodel則用來顯示資料
		//與dto不同，viewmodel還可以有attribute，來定義一些顯示的資訊
		[Display(Name ="使用者帳號")]
		public string UserName { get; set; }
	}

	public class UserCreateViewModel
	{
		public string UserName { get; set; }
	}

	public class UserUpdateViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
	}

}
