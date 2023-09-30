using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;



namespace BS_Adoga.Service

{
    public class HashService
    {
        //.NET Framework 密碼編譯服務 : https://docs.microsoft.com/zh-tw/dotnet/standard/security/cryptographic-services#hash_values


        //MD5 Class - https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.md5?view=netframework-4.8
        public static string MD5Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    //"x2" format each one as a hexadecimal string
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString();
        }

        public static string MD5HashBase64(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);
                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);

                result = Convert.ToBase64String(encryption);
            }

            return result;
        }

        //SHA1演算法雜湊大小是160 位元
        public static string SHA1Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA1 sha1 = SHA1.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                byte[] encryption = sha1.ComputeHash(byteArray);


                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }

        //SHA256演算法雜湊大小是256位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha256?view=netframework-4.8


        //SHA384演算法雜湊大小是256位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha384?view=netframework-4.8


        //SHA512演算法雜湊大小為512位元 : https://docs.microsoft.com/zh-tw/dotnet/api/system.security.cryptography.sha512?view=netframework-4.8
        public static string SHA512Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA512 sha512 = SHA512.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                byte[] encryption = sha512.ComputeHash(byteArray);


                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }
    }
}
