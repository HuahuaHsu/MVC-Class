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

		public void ActiveRegister(int memberId, string confirmCode)
		{
			//1.根據 memberId 取得會員資料
			var member = Load(memberId);
			//2.如果會員資料是否存在
			if (member == null)
			{
				throw new Exception("會員資料不存在");
			}
			//比對 confirmCode 是否正確
			if (member.NewMemberConfirmCode != confirmCode)
			{
				throw new Exception("驗證碼不正確");
			}
			//3.如果正確，將 IsConfirmed 設為 true
			member.IsConfirmed = true;
			member.NewMemberConfirmCode = null;

			_repository.Update(member);

		}

		private MemberDto Load(int memberId)
		{
			return _repository.GetById(memberId);
		}
	}
}
