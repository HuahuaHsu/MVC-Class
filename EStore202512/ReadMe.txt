[V]連結資料庫
	設定EfModels
	設定連線字串
	註冊 DbContext 到 DI
=========
開發會員機制
[working on]add 會員註冊功能
	url: /Auth/Register/
	[V] 雜湊密碼的公用程式
		安裝 BCrypt.Net-Next 套件
		/Models/Infra/HashUtility.cs
			static string HashPassword(string password)
			static bool VerifyPassword(string password, string hashedPassword)

	[V] ViewModel, Dto, RegisterViewModel 轉 RegisterDto 的擴充方法
		RegisterViewModel class
			Account, Password, ConfirmPassword, Name, Email, Mobile properties

		RegisterDto class
			Account, Password, Name, Email, Mobile, HashedPassword, NewMemberConfirmCode, IsConfirmed properties

		RegisterViewModelExtension class
			RegisterDto ToDto(this RegisterViewModel vm)

	[V] 建立Service/Repository並註冊到DI
		IMemberRepository interface
			void Register(RegisterDto dto)
			bool IsExists(string account)
		
		MemberRepository class 實作 IMemberRepository

		Result class
			IsSuccess, ErrorMessage
			static Result Success();
			static Result Fail(string errorMessage)

		AuthService class
			ctor(IMemberRepository repo)
			Result Register(RegisterDto dto)
			// 未來會再加Login, Logout,..

	[V] AuthController
		Register()
			Register.cshtml
			RegisterConfirm.cshtml
		Register(RegisterViewModel vm)

		在Layout加入 註冊連結

	[working on] 實作新會員 Email 確認功能

		url: /Auth/ActiveRegister?memberId=99&confirmcode=xxxx
		MemberDto class

		MemberRepository
			MemberDto GetById(int memberId)
			void Update(MemberDto dto)

		AuthService
			void ActiveRegister(int memberId, string confirmCode)
			MemberDto Load(int memberId)


		AuthController
			ActiveRegister(int memberId, string confirmCode)

		ActiveRegister.cshtml
	

[]開發線上購物
[]註冊新會員後，必須發信給會員，內容確認連結

