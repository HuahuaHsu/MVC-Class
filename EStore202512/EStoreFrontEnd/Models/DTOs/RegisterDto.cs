namespace EStoreFrontEnd.Models.DTOs
{
	public class RegisterDto
	{
		public string Account { get; set; }

		public string HashedPassword { get; set; }

		public string Email { get; set; }

		public string Name { get; set; }

		public string Mobile { get; set; }

		public bool? IsConfirmed { get; set; }

		public string NewMemberConfirmCode { get; set; }

		public string Password { get; set; }
	}
}
