namespace EStoreFrontEnd.Models.DTOs
{
	public class ChangePasswordDto
	{
		public int Id { get; set; }

		public string OrigPassword { get; set; }

		public string NewPassword { get; set; }

		public string HashedPassword { get; set; }

	}
}
