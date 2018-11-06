using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Areas.Management.Helpers
{
    public static class InternalHelpers
    {
        /// <summary>
        /// Hàm lấy nội dung tóm tắt của câu hỏi, kiểm tra có công thức toán học và hình ảnh hay không
        /// </summary>
        /// <param name="noiDung"></param>
        /// <returns></returns>
        public static string LayNoiDungTomTam(string noiDung, int len, bool laCauHoiDienGiaTri, bool laCauHoiNhom, int soCauHoiCon, bool showPrefix = false)
        {
            string prefix = "";
            if (laCauHoiDienGiaTri)
                prefix = "[GT] ";
            if (laCauHoiNhom)
                prefix = "[N" + soCauHoiCon + "] ";

            int index1 = noiDung.IndexOf("\\(");
            int index2 = noiDung.IndexOf("\\)");
            int index3 = noiDung.IndexOf("<img");
            var img = "";
            string returnVal = "";
            string firstChars = "";
            string congThucToan = "";

            //Nếu trong nội dung có chứa cả ảnh và cả công thức toán
            if (index1 >= 0 && index2 >= 0 && index3 >= 0)
            {
                img = GetImageInHTMLString(noiDung);
                congThucToan = noiDung.Substring(index1, index2 - index1 + 2);
                int length = (len > index1 && len > index3) ? (index1 > index3 ? index3 : index1) : len;
                firstChars = Common.Helpers.CommonHelper.RemoveHtmlTags(noiDung, length);
                if (index3 > index1)
                {
                    returnVal = firstChars + "... " + congThucToan + " ... " + img + "...";
                }
                else
                {
                    returnVal = firstChars + "... " + congThucToan + " ... " + img + "...";
                }
                if (showPrefix)
                    return prefix + returnVal;
                else
                    return returnVal;
            }
            //Nếu trong nội dung có chứa ảnh
            if (index3 >= 0)
            {
                img = GetImageInHTMLString(noiDung);
                int length = len > index3 ? index3 : len;
                firstChars = Common.Helpers.CommonHelper.RemoveHtmlTags(noiDung, length);
                returnVal = firstChars + "... " + img + "...";
                if (showPrefix)
                    return prefix + returnVal;
                else
                    return returnVal;
            }
            //Nếu trong nội dung có chứa công thức toán
            if (index1 >= 0 && index2 >= 0)
            {
                congThucToan = noiDung.Substring(index1, index2 - index1 + 2);
                int length = len > index1 ? index1 : len;
                firstChars = Common.Helpers.CommonHelper.RemoveHtmlTags(noiDung, length);
                returnVal = firstChars + "... " + congThucToan + "...";
                if (showPrefix)
                    return prefix + returnVal;
                else
                    return returnVal;
            }
            returnVal = Common.Helpers.CommonHelper.RemoveHtmlTags(noiDung, len) + "...";
            if (showPrefix)
                return prefix + returnVal;
            else
                return returnVal;
        }
        private static List<string> GetImagesInHTMLString(string htmlString)
        {
            List<string> images = new List<string>();
            string pattern = @"<(img)\b[^>]*>";
            //string pattern2 = "<img.+?src=[\"'](.+?)[\"'].+?>";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);
            if (matches.Count > 0)
            {
                for (int i = 0, l = matches.Count; i < l; i++)
                {
                    images.Add(matches[i].Value);
                }
            }
            return images;
        }
        private static string GetImageInHTMLString(string htmlString)
        {
            string pattern = @"<(img)\b[^>]*>";
            //string pattern2 = "<img.+?src=[\"'](.+?)[\"'].+?>";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);
            if (matches.Count > 0)
            {
                return matches[0].Value;
            }
            return "";
        }
        /// <summary>
        /// Xóa thẻ html từ string, ngoại trừ img, sub và sub
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveUnwantedTagsFromString(string html)
        {
            string acceptable = "img|sub|sup|p";
            string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:(["",']?).*?\1?)?)*\s*/?>";
            return Regex.Replace(html, stringPattern, "");
        }
    }
}