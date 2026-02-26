namespace EStoreFrontEnd.Models.Infra
{
	public class HashUtility
	{
		/// <summary>
		/// 將明文密碼進行 BCrypt 雜湊處理 (包含自動生成 Salt)
		/// </summary>
		/// <param name="password">使用者輸入的明文密碼</param>
		/// <returns>雜湊後的密碼字串</returns>
		public static string HashPassword(string password)
		{
			// HashPassword 方法預設會自動產生並套用 Salt
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		/// <summary>
		/// 驗證明文密碼與資料庫中的雜湊密碼是否相符
		/// </summary>
		/// <param name="password">使用者輸入的明文密碼</param>
		/// <param name="hashedPassword">資料庫中儲存的雜湊密碼</param>
		/// <returns>密碼是否正確</returns>
		public static bool VerifyPassword(string password, string hashedPassword)
		{
			// Verify 方法會自動提取 hashedPassword 中的 Salt 並進行比對
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}
