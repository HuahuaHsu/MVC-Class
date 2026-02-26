using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Infra;
using EStoreFrontEnd.Models.Repositories;

namespace EStoreFrontEnd.Models.Services
{
	public class AuthService
	{
		private readonly IMemberRepository _repository;

		public AuthService(IMemberRepository repository) 
		{
			_repository = repository;
		}

		public Result Register(RegisterDto dto)
		{
			dto.HashedPassword = HashUtility.HashPassword(dto.Password);

			dto.IsConfirmed = false;
			dto.NewMemberConfirmCode = Guid.NewGuid().ToString();

			if (_repository.IsExists(dto.Account))
			{
				return Result.Failure("帳號已存在");
			}
			_repository.Register(dto);
			return Result.Success();
		}
	}
}
