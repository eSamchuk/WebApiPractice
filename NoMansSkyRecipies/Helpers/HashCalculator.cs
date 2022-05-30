using System.Security.Cryptography;
using System.Text;

namespace NoMansSkyRecipies.Helpers
{
    public static class HashCalculator
    {
        /// <summary>
        /// calculates SHA256 hash of raw string
        /// </summary>
        /// <param name="input">String to calculate hash from</param>
        /// <returns>SHA256 hash</returns>
        public static string GetHashSha256(string input)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

            }

            return sb.ToString();
        }

        public static string GetHash(this string input)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (var b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

            }

            return sb.ToString();
        }


    }
}
