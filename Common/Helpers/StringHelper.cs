using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class StringHelper
    {//Hàm chuyển chuỗi ký tự thông thường sang chuỗi ký tự được mã hóa dạng MD5
        public static string StringToMd5(string value)
        {
            var md5 = string.Empty;
            var md5Hasher = new MD5CryptoServiceProvider();
            var encoder = new System.Text.UTF8Encoding();
            byte[] hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(value));
            return hashedBytes.Aggregate(md5, (current, b) => current + b.ToString("X2"));
        }
        public static string stringToSHA256(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        // ma hoa 512 bit
        public static string stringToSHA512(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        // ma hoa 384bit
        public static string stringTo384(string inputString)
        {
            SHA384 sha384 = SHA384Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha384.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        //get chuoi ra chuoi khỏe mạnh
        // private
        private static string GetStringFromHash(byte[] hash)
        {
            var s = string.Empty;
            return hash.Aggregate(s, (current, b) => current + b.ToString("X5"));
        }
        //Hàm xóa các ký tự đặc biệt nhằm chống tấn công SQL injection
        public static string KillChars(string strInput)
        {
            string result = "";
            if (!String.IsNullOrEmpty(strInput))
            {
                string[] arrBadChars = new string[] { "select", "drop", "--", "insert", "delete", "xp_", "sysobjects", "syscolumns", "'", "1=1", "truncate", };//Loại bỏ "or, table" để tránh lỗi không nhập được những từ có chứa "or, table"
                result = strInput.Trim().Replace("'", "''");
                result = result.Replace("%20", " ");
                for (int i = 0; i < arrBadChars.Length; i++)
                {
                    string strBadChar = arrBadChars[i].ToString();
                    result = result.Replace(strBadChar, "");
                }
            }
            return result;
        }
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (rng == null) rng = new Random();
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = rng.Next(i, buffer.Count);
                yield return buffer[j];
                buffer[j] = buffer[i];
            }
        }
        //Tổng hợp chi tiết các ký tự có dấu và không dấu tiếng Việt
        private static readonly string[] VietNamChar = new string[] 
        { 
            "aAeEoOuUiIdDyY", 
            "áàạảãâấầậẩẫăắằặẳẵ", 
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", 
            "éèẹẻẽêếềệểễ", 
            "ÉÈẸẺẼÊẾỀỆỂỄ", 
            "óòọỏõôốồộổỗơớờợởỡ", 
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", 
            "úùụủũưứừựửữ", 
            "ÚÙỤỦŨƯỨỪỰỬỮ", 
            "íìịỉĩ", 
            "ÍÌỊỈĨ", 
            "đ", 
            "Đ", 
            "ýỳỵỷỹ", 
            "ÝỲỴỶỸ" 
        };
        //Lọc các ký tự có dấu tiếng Việt về dạng không dấu
        public static string StringFilter(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim().Replace("\"", "");
                str = str.Trim().Replace(".", "");
                str = str.Trim().Replace(":", "");
                str = str.Trim().Replace(",", "");
                str = str.Trim().Replace(";", "");
                str = str.Trim().Replace(" - ", " ");
                str = str.Trim().Replace("/", "-");
                str = Regex.Replace(str, " ", "-");
                str = Regex.Replace(str, "--", "-");
                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                        str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
                return str.ToLower();
            }
            else
            {
                return string.Empty;
            }
        }
        //Cái này dùng khi tạo thư mục, tệp tin
        public static string StringFilter2(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim().Replace("\"", "");
                //str = str.Trim().Replace(".", "");
                str = str.Trim().Replace(":", "");
                str = str.Trim().Replace(",", "");
                str = str.Trim().Replace(";", "");
                str = str.Trim().Replace(" - ", " ");
                //str = str.Trim().Replace("/", "-");
                str = Regex.Replace(str, " ", "-");
                str = Regex.Replace(str, "--", "-");
                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                        str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
                return str.ToLower();
            }
            else
            {
                return string.Empty;
            }
        }
        public static string CreateRandomString(int len)
        {
            if (rng == null) rng = new Random();
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            char[] chars = new char[len];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < len; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * rng.NextDouble())];
            }
            return new string(chars);
        }
        public static string CreateRandomStringByNumerical(int len)
        {
            if (rng == null) rng = new Random();
            string _allowedChars = "0198723456";
            char[] chars = new char[len];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < len; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * rng.NextDouble())];
            }
            return new string(chars);
        }
        public static string CreateRandomStringByNumerical2(int len)
        {
            if (rng == null) rng = new Random();
            string _allowedChars = "0198723456";
            char[] chars = new char[len];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < len; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * rng.NextDouble())];
            }
            return new string(chars);
        }
        public static int newSeed;
        public static Random rng;
        public static string RandomString2(int n)
        {
            if (rng == null) rng = new Random();
            var _allowedChars = ("0198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456019872345601987234560198723456").ToCharArray();
            Shuffle2(_allowedChars);
            return new string(_allowedChars, 0, n);
        }

        public static void Shuffle2(char[] array)
        {
            if (rng == null) rng = new Random();
            for (int n = array.Length; n > 1; )
            {
                int k = rng.Next(n);
                --n;
                char temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        //Trim Richtext string. Xóa các khoảng trắng trước và sau văn bản HTML
        public static string TrimHTML(string rawHTML)
        {
            Regex rgx = new Regex(@"<(\w+)\s*.*?>\s*?</\1>");
            string result = rgx.Replace(rawHTML.Trim(),"");
            Regex rgx1= new Regex(@"^<p>(.*)</p>");
            string filteredString=result;
            if(rgx1.IsMatch(result.Trim()))
            filteredString=result.Substring(3,result.Trim().Length-7);
            return filteredString.Trim();
        }
        public static string SubString(string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return value.Substring(0, length > value.Length ? value.Length : length);
        }
        public static string RemoveHtmlTags(string value, int length = 0)
        {
            if (value == null) return "";
            string result = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            result = Regex.Replace(result, @"\s{2,}", ". ");
            string dots = result.Length > length ?"...":"";
            if (length != 0)
                result = SubString(result, length)+dots;
            return result;
        }
        public static string ConvertToPhuongAn(int phuongAn)
        {
            if (phuongAn == 1) return "A";
            if (phuongAn == 2) return "B";
            if (phuongAn == 3) return "C";
            if (phuongAn == 3) return "D";
            return "A";
        }
        public static string ConvertIdToMa(decimal id)
        {
            return id.ToString("0000000");
        }
        public static DateTime? MMYYYY(string input)
        {
            try
            {
                input = KillChars(input);
                input = String.Format("0{0,1}", input);
                input = input.Substring(input.Length - 7);
                DateTime output = DateTime.ParseExact(input, "MM/yyyy", null);
                return output;
            }
            catch
            {
                return (DateTime?)null;
            }
        }
        // cắt chuỗi để co ngắn hiển thị 
        // visibleExtension true sẽ để lại đuối của file ngược lại thì không
        public static string ShorStr(string str, int maxLength, bool visibleExtension)
        {
            if (str == null) return "";
            if (maxLength < 5) return str;
            //if (str.Length >= maxLength - 3) return str.Substring(0, maxLength - 3) + "...";
            
            var exten = visibleExtension== true? Path.GetExtension(str): "";
            if (str.Length >= maxLength - 5) return str.Substring(0, maxLength - 5) + "..."+exten;
            return str;
        }
    }
}
