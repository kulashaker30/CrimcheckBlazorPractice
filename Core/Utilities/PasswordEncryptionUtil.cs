
using System;
using System.Security.Cryptography;
using System.Text;

namespace OnlineClinic.Core.Utilities
{
    public interface IPasswordEncryptionUtil
    {
        string Encrypt(string plainText);

        string Decrypt(string encryptedText);
    }

    public class PasswordEncryptionUtil : IPasswordEncryptionUtil
    {
        private readonly string _securityKeyStr;

        public PasswordEncryptionUtil(string securityKeyStr) => _securityKeyStr = securityKeyStr;

        public string Encrypt(string plainText)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(plainText);

            MD5CryptoServiceProvider mdrSvc = new MD5CryptoServiceProvider();
            byte[] securityKeyArray = mdrSvc.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKeyStr));
            mdrSvc.Clear();

            var desSvc = new TripleDESCryptoServiceProvider();
            desSvc.Key = securityKeyArray;
            desSvc.Mode = CipherMode.ECB;
            desSvc.Padding = PaddingMode.PKCS7;


            var cryptoTransform = desSvc.CreateEncryptor();
            byte[] resultArray = cryptoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            desSvc.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] arr = Convert.FromBase64String(encryptedText);
            MD5CryptoServiceProvider md5Svc = new MD5CryptoServiceProvider();

            byte[] secKeyArr = md5Svc.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKeyStr));
            md5Svc.Clear();

            var desSvc = new TripleDESCryptoServiceProvider();
            desSvc.Key = secKeyArr;
            desSvc.Mode = CipherMode.ECB;
            desSvc.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = desSvc.CreateDecryptor();
            byte[] resultArr = objCrytpoTransform.TransformFinalBlock(arr, 0, arr.Length);
            desSvc.Clear();
            return UTF8Encoding.UTF8.GetString(resultArr);
        }
    }
}