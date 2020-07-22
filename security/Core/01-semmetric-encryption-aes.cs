using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;

namespace semmetric_encryption_aes
{
    [TestClass]
    public class AesRunner
    {
        [TestMethod]
        public void Aes()
        {
            var dataToEncrypt = GetBytes("The secret");
            var key = GetBytes(Guid.Parse("514bc916-1e8f-4743-8e44-c9e77f0c38eb").ToString());
            var iv = GetBytes(Guid.Parse("7ae000e0-0f4d-4bf7-944c-386114fa8fc8").ToString());
            var crypted = Encrypt(dataToEncrypt, key, iv);
            Console.WriteLine($" Encrypted text {GetText(crypted)}");
            var encrypted = Decrypt(crypted, key, iv);
            Console.WriteLine($" Decrypted text {GetText(encrypted)}");
        }

        public byte[] Encrypt(byte[] dataToEncrypt, byte[]key, byte[] iv)
        {
            using var aes = new AesCryptoServiceProvider
            {
                Key = key,
                IV = iv
            };
            using var ms = new MemoryStream();
            var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
            cryptoStream.FlushFinalBlock();

            return ms.ToArray();
        }

        public byte[] Decrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using var aes = new AesCryptoServiceProvider
            {
                Key = key,
                IV = iv
            };
            using var ms = new MemoryStream();
            var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
            cryptoStream.FlushFinalBlock();

            return ms.ToArray();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


        static string GetText(byte[] strAsByte)
        {
            return System.Text.Encoding.Default.GetString(strAsByte);
        }
    }
}
