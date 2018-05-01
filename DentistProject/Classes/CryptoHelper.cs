using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DentistProject.Helpers
{
    public class CryptoHelper
    {
        public static string key = "NKU-SA";

        public static string EncryptRSA(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException();
            RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
            return Convert.ToBase64String(dec.Encrypt(ConvertToByteArray(plainText), false));
        }

        public static string DecryptRSA(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException();
            RSACryptoServiceProvider dec = new RSACryptoServiceProvider();
            byte[] aryDizi = Convert.FromBase64String(plainText + key);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] aryDonus = dec.Decrypt(aryDizi, false);
            return UE.GetString(aryDonus);
        }
        public static byte[] ConvertToByteArray(string plainText)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(plainText);
        }

        public static string EncrytCustomMD5(string plainText)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                return string.Join("", BitConverter.ToString(md5.ComputeHash(ConvertToByteArray(plainText + key))).Split('-'));
            }
        }
    }
}