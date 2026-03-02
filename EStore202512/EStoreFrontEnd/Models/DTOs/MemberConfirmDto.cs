namespace EStoreFrontEnd.Models.DTOs
{

	public class MemberConfirmDto
	{
		public int Id { get; set; }

		public bool? IsConfirmed { get; set; }

		public string NewMemberConfirmCode { get; set; }

	}
}
