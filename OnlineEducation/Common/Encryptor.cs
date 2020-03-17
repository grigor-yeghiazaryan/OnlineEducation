namespace OnlineEducation.Common
{
    public class Encryptor
    {
        public static string Encrypt(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
                return null;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            var hash = System.Text.Encoding.ASCII.GetString(data);
            return hash;
        }
    }
}
