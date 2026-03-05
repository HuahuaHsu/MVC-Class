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

	[V] 實作新會員 Email 確認功能

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

		更名

		MemberDto -> MemberConfirmDto, 只包含必要屬性
		AuthService.ActiveRegister() 不必改
		MemberRepository.Update -> UpdateConfirmStatus()

	[working on] 實作 登入/登出網站
	只有帳密正確而且已正式開通的會員才允許登入, 實作之前, 請先各別建立一個已/未開通的會員記錄,方便測試
	- 修改MemberDto(加HashedPassword), add LoginViewModel
	- modify MemberRepository
		add Task<MemberDto> GetMemberByAccountAsync(string account)

	- modify AuthService

		add Task<Result> LoginAsync(string account, string password)
		- 檢查帳號是否存在
		- 檢查密碼是否正確
		- 檢查會員是否已開通
		- 若以上都正確, 則建立 Cookie	

	- modify AuthController
		add Login() actions
			Login.cshtml(template: create)
		add Logout() action

	- modify _Layout page, add "Login/Logout" links

	- modify Program.cs, add Cookie Authenthcation
		prompt: 在 Program.cs 加入 Cookie 認證, cookie name是estoreDemo
	
	- 針對/Home/Privacy/, add "Authorize" attribute, 若沒登入過,會自動導向到/Auth/Login
	
	- modify AuthController, add HttpGet Logout action


[V]實作 修改密碼 /Members/ChangePassword
	- add 會員中心頁, /Members/Index
	- add ChangedPasswordDto: Id, OrigPassword, NewPassword, HashedPassword
	- add ChangedPasswordViewModel: OrigPassword, NewPassword, ConfirmPassword
	- modify MemberRepository
		- add ChangePasswordAsync(int memberId, string hashedPassword)

	- add MemberService
		.ChangePasswordAsync(ChangedPasswordDto dto)
		在 Program.cs 註冊 MemberService 到 DI

	- modify MembersController
		- add ChangePassword actions, 要加Authorize
		- add "ChangePassword" view page, 更新成功後, 導向到 Members/Index, 將訊息顯示在 Index page上
	-modify Members/Index.cshtml, 顯示訊息(來自 ChangePassword or EditProfile Pages)
	
前台購物車功能
[V] 建立商品清單頁並設為網站首頁
	- create /wwwroot/uploads/ folder, 並放入商品圖片
	- dto
		ProductDto: Id, Name, Price, ImageUrl
	- view model
		add Models/ViewModels/ProductIndexItemViewModel.cs
		add ProductDtoExtension,撰寫擴充方法

	- add ProductRepository
		add interface IProductRepository
			List<ProductDto> GetAll()
		add class ProductRepository : IProductRepository
	- add ProductService
		ctor(IProductRepository repo)
		IEnumerable<ProductDto> GetAllForIndex()

	- add ProductsController(新增空白controller), add Index action

	- add Index.cshtml, 
		- 撰寫js,實作加入購物車功能Default
		- 移除 AddNew hyperlink
		- 只有在已登入狀況下,才顯示 'add to cart' button


	- add CartController, 實作將商品加入購物車的功能
	- modify Program.cs , 預設為 Products/Index
		- modify _layout ,修改home的hyperlink
	

[V] 實作 顯示購物車內容 /Cart/Index

   -add Dto classes:

   CartDto: Id, List<CartItemDto> Items

   CartItemDto: Id, ProductId, ProductName, UnitPrice, Quantity, Subtotal

  -add ViewModel classes (顯示一筆購物車資料及其明細):

   CartItemViewModel: Id, ProductId, ProductName, UnitPrice, Quantity, Subtotal

   CartIndexViewModel: List<CartItemViewModel> Items, TotalAmount

   -add CartDtoExtensions (撰寫擴充方法):

   ToViewModel(this CartDto dto)

  -add CartRepository:


  -add CartService:


  -modify CartController:

   -modify AddItem action: 將商品加入購物車

   -add Info action: 顯示購物車明細（此時數量增減尚無法運作，需等 UpdateItem action 實作）

   -add Cart/Index view page: 使用 CartIndexViewModel

   撰寫 JS code: 實作加減數量的功能

   -add UpdateItem action

   -modify _Layout view page: 顯示購物車資訊

[working on] 實作結帳 / Cart/Checkout
	- add CheckoutViewModel
	- modify CartController, 
		add Checkout() action
		add ConfirmCheckout() action

[]實作修改會員資料 /Members/EditProfile
[]忘記密碼
[]重設密碼
[]註冊新會員後，必須發信給會員，內容確認連結

