using EStoreFrontEnd.Models.DTOs;
using EStoreFrontEnd.Models.EfModels;

namespace EStoreFrontEnd.Models.Repositories
{
	public interface IMemberRepository
	{
		void Register(RegisterDto dto);
		bool IsExists(string account);
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
	}
}
