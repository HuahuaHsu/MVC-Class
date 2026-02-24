namespace WebApplication2.ViewModels
{
	public class UserItemViewModel
	{
		public int Id { get; set; }
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
