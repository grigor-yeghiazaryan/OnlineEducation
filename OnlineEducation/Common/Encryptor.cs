using System.Security.Cryptography;
using System.Text;

namespace OnlineEducation.Common
{
    public class Encryptor
    {
        public static string Encrypt(string inputString)
        {
            string hash = CalculateMD5Hash(inputString);

            return hash;

            //if (string.IsNullOrWhiteSpace(inputString))
            //    return null;

            //byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            //var hash = System.Text.Encoding.ASCII.GetString(data);
            //return hash;
        }



        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
