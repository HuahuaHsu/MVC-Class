using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;

namespace EStoreFrontEnd.Models.Repositories
{
	public interface IMemberRepository
	{
		void Register(RegisterDto dto);
		bool IsExists(string account);

		MemberDto GetById(int memberId);
		void Update(MemberDto dto);
	}

	public class MemberRepository : IMemberRepository
	{
		private readonly EStoreContext _dbContext;

		public MemberRepository(EStoreContext context)
		{
			_dbContext = context;
		}

		public bool IsExists(string account)
		{
			// 這裡可以使用 EF Core 的 Any 方法來檢查帳號是否存在
			return _dbContext.Members.Any(m => m.Account == account);
		}

		public void Register(RegisterDto dto)
		{
			// 這裡可以將 RegisterDto 轉換為 Member 實體，然後使用 EF Core 的 Add 方法來新增會員資料
			var member = new Member
			{
				Account = dto.Account,
				HashedPassword = dto.HashedPassword,
				Email = dto.Email,
				Name = dto.Name,
				Mobile = dto.Mobile,
				IsConfirmed = dto.IsConfirmed ?? false, // 預設為 false
				NewMemberConfirmCode = dto.NewMemberConfirmCode
			};
			_dbContext.Members.Add(member);
			_dbContext.SaveChanges();
		}

		public MemberDto GetById(int memberId)
		{
			var member = _dbContext.Members.Find(memberId);
			if (member == null)
			{
				return null;
			}

			return new MemberDto
			{
				Id = member.Id,
				Account = member.Account,
				Email = member.Email,
				Name = member.Name,
				Mobile = member.Mobile,
				IsConfirmed = member.IsConfirmed,
				NewMemberConfirmCode = member.NewMemberConfirmCode,
				ResetPasswordConfirmCode = member.ResetPasswordConfirmCode
			};
		}

		public void Update(MemberDto dto)
		{
			// 這裡可以使用 EF Core 的 Find 方法來找到要更新的會員資料，然後更新其屬性，最後使用 SaveChanges 方法來保存更改
			var member = _dbContext.Members.Find(dto.Id);
			if (member == null)
			{
				return;
			}

			member.Email = dto.Email;
			member.Name = dto.Name;
			member.Mobile = dto.Mobile;
			member.IsConfirmed = dto.IsConfirmed;
			member.NewMemberConfirmCode = dto.NewMemberConfirmCode;
			member.ResetPasswordConfirmCode = dto.ResetPasswordConfirmCode;

			_dbContext.SaveChanges();
		}
	}
}
