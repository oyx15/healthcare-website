using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Test_Website
{
    /// <summary>
    /// Summary description for Security
    /// </summary>
    public class Security
    {
        private RijndaelManaged myRijndael = new RijndaelManaged();
        private int iterations;
        private byte[] salt;
        private MD5 md5Hash = MD5.Create();

        public Security()
        {
            string strPassword = "Crypto";
            myRijndael.BlockSize = 128;
            myRijndael.KeySize = 128;
            myRijndael.IV = Convert.FromBase64String("pQDS6cFyGuDoStZgxHIA8w==");
            myRijndael.Padding = PaddingMode.PKCS7;
            myRijndael.Mode = CipherMode.CBC;
            iterations = 1000;
            salt = System.Text.Encoding.UTF8.GetBytes("cryptography123example");
            myRijndael.Key = GenerateKey(strPassword);
        }

        public  string Encrypt(string strPlainText)
        {
            byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
            ICryptoTransform transform = myRijndael.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);
            return Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            string plaintext = null;
            var key = Convert.ToBase64String(myRijndael.Key);
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            // Check arguments. 
            if (encryptedBytes == null || encryptedBytes.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || myRijndael.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (myRijndael.IV == null || myRijndael.IV.Length <= 0)
                throw new ArgumentNullException("Key");

            ICryptoTransform decryptor = myRijndael.CreateDecryptor(myRijndael.Key, myRijndael.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                        if (plaintext == null || plaintext.Length <= 0)
                            throw new ArgumentNullException("No output.");

                    }
                }
            }

            return plaintext;
        }


        public static byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];
            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }
            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        private byte[] GenerateKey(string strPassword)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(strPassword), salt, iterations);
            return rfc2898.GetBytes(128 / 8);
        }

        public string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

    }

    public class ClsCrypto
    {
        private RijndaelManaged myRijndael = new RijndaelManaged();
        private int iterations;
        private byte[] salt;

        public ClsCrypto()
        {
            string strPassword = "Crypto";
            myRijndael.BlockSize = 128;
            myRijndael.KeySize = 128;
            myRijndael.IV = Convert.FromBase64String("pQDS6cFyGuDoStZgxHIA8w==");
            myRijndael.Padding = PaddingMode.PKCS7;
            myRijndael.Mode = CipherMode.CBC;
            iterations = 1000;
            salt = System.Text.Encoding.UTF8.GetBytes("cryptography123example");
            myRijndael.Key = GenerateKey(strPassword);
        }

        public  string Encrypt(string strPlainText)
        {
            byte[] strText = new System.Text.UTF8Encoding().GetBytes(strPlainText);
            ICryptoTransform transform = myRijndael.CreateEncryptor();
            byte[] cipherText = transform.TransformFinalBlock(strText, 0, strText.Length);
            return Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            string plaintext = null;
            var key = Convert.ToBase64String(myRijndael.Key);
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            // Check arguments. 
            if (encryptedBytes == null || encryptedBytes.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || myRijndael.Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (myRijndael.IV == null || myRijndael.IV.Length <= 0)
                throw new ArgumentNullException("Key");

            ICryptoTransform decryptor = myRijndael.CreateDecryptor(myRijndael.Key, myRijndael.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream 
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                        if (plaintext == null || plaintext.Length <= 0)
                            throw new ArgumentNullException("No output.");

                    }
                }
            }

            return plaintext;
        }


        public static byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];
            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }
            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        private byte[] GenerateKey(string strPassword)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(System.Text.Encoding.UTF8.GetBytes(strPassword), salt, iterations);
            return rfc2898.GetBytes(128 / 8);
        }
    }

    public class HashMD5
    {
        private MD5 md5Hash = MD5.Create();
        public string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}