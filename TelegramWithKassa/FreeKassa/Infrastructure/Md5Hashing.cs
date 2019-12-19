using System.Security.Cryptography;
using System.Text;

namespace FreeKassa.Infrastructure
{
    public class Md5Hashing
    {
        public string GetMd5Hash(string dataToHashing)
        {
            StringBuilder sBuilder = new StringBuilder();
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(dataToHashing));

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
