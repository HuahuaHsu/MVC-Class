using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.Infra;
using EStoreFrontEnd.Models.Repositories;

namespace EStoreFrontEnd.Models.Services
{
	public class MemberService
	{
		private readonly IMemberRepository _Repository;

		public MemberService(IMemberRepository Repository)
		{
			_Repository = Repository;
		}

		public async Task<MemberDto> GetByAccount(string account)
		{
			return await _Repository.GetMemberByAccountAsync(account);
		}


		public async Task<Result> ChangePasswordAsync(ChangePasswordDto dto)
		{
			//判斷原始密碼是否正確
			var member =_Repository.GetById(dto.Id);
			if (member == null)return Result.Failure("找不到會員");
			if (HashUtility.VerifyPassword(dto.OrigPassword, member.HashedPassword)) return Result.Failure("原始密碼不正確");

			//將新密碼進行Hash處理
			var hashedNewPassword = HashUtility.HashPassword(dto.NewPassword);

			//更新資料庫中的密碼
			await _Repository.ChangePasswordAsync(dto.Id, hashedNewPassword);
			return Result.Success();
		}
	}
}
