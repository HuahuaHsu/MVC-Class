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

	[working on] ViewModel, Dto, RegisterViewModel 轉 RegisterDto 的擴充方法
		RegisterViewModel class
			Account, Password, ConfirmPassword, Name, Email, Mobile properties

		RegisterDto class
			Account, Password, Name, Email, Mobile, HashedPassword, NewMemberConfirmCode, IsConfirmed properties

		RegisterViewModelExtension class
			RegisterDto ToDto(this RegisterViewModel vm)

	[] 建立Service/Repository
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

	[] AuthController
		Register()
			Register.cshtml
			RegisterConfirm.cshtml
		Register(RegisterViewModel vm)


[]開發線上購物
