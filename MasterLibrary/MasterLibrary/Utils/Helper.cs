using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace MasterLibrary.Utils
{
    public class Helper
    {
        /// <summary>
        /// Chuyển đổi số ra tiền kiểu Việt Nam
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string FormatVNMoney(decimal money)
        {
            if (money == 0)
            {
                return "0 ₫";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#} ₫", money);
        }

        /// <summary>
        /// Chuyển sang chuỗi sang Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Chuyển chuỗi sang MD5Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        /// <summary>
        /// Mã hoá mật khẩu
        /// </summary>
        /// <param name="_password"></param>
        /// <returns></returns>
        public static string HashPassword(string _password)
        {
            return MD5Hash(Base64Encode(_password));
        }
    }
}
