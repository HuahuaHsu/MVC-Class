namespace API.DTO
{
	public class EmployeeDTO
	{
		public int EmployeeId { get; set; }

		public required string LastName { get; set; }

		public required string FirstName { get; set; }

		public string? Title { get; set; }
	}
}