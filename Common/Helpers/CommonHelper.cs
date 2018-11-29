using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace Common.Helpers
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class CommonHelper
    {
        #region Methods
        /// <summary>
        /// Hàm đổi ký tự tiếng Việt có dấu sang không dấu
        /// </summary>
        /// <param name="unicodeString"></param>
        /// <returns></returns>
        public static string DocType = "NoWord";
        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900); //EDIT: i've typed 400 instead 900
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
        public static bool IsNull(object o)
        {
            return (o == null);
        }
        public static string GetText(long? val, string defaultValue)
        {
            if (val == null) return defaultValue;
            return val.Value.ToString();
        }
        public static string GetText(DateTime? val, string defaultValue)
        {
            if (val == null) return defaultValue;
            return val.Value.ToString("dd/MM/yyyy");
        }
        public static string GetText(DateTime? val, string format, string defaultValue)
        {
            if (val == null) return defaultValue;
            return val.Value.ToString("format");
        }
        public static string GetText(double? val, string defaultValue)
        {
            if (val == null) return defaultValue;
            return val.Value.ToString();
        }
        public static string RemoveMarks(string unicodeString)
        {
            try
            {
                //Remove VietNamese character
                unicodeString = unicodeString.ToLower();
                unicodeString = Regex.Replace(unicodeString, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                unicodeString = Regex.Replace(unicodeString, "[éèẻẽẹêếềểễệ]", "e");
                unicodeString = Regex.Replace(unicodeString, "[iíìỉĩị]", "i");
                unicodeString = Regex.Replace(unicodeString, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                unicodeString = Regex.Replace(unicodeString, "[úùủũụưứừửữự]", "u");
                unicodeString = Regex.Replace(unicodeString, "[yýỳỷỹỵ]", "y");
                unicodeString = Regex.Replace(unicodeString, "[đ]", "d");

                //Remove space
                unicodeString = StandardSpace(unicodeString);
                unicodeString = unicodeString.TrimEnd().TrimStart();

                return unicodeString;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string ToURL(string unicodeString, int length = 10)
        {
            if (length > 0)
                return RemoveSpecialCharacters(RemoveMarks(unicodeString)).Replace(" ", "-").Replace("--", "-") + "-" +
                       RandomString(length);
            return RemoveSpecialCharacters(RemoveMarks(unicodeString)).Replace(" ", "-").Replace("--", "-");
        }
        /// <summary>
        /// Hàm xóa các ký tự đặc biệt, chỉ để lại các ký tự chuẩn
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }
        /// <summary>
        /// Hàm tạo 1 chuỗi ngẫu nhiên các ký tự có độ dài length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            string outValue = string.Empty;
            char[] charArray = {
                                   'a', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'd', 's', 'f', 'g', 'h', 'j', 'k'
                                   , 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                   '0'
                               };
            var r = new Random();
            for (int i = 0; i < length; i++)
            {
                outValue += charArray[r.Next(charArray.Length)];
            }
            return outValue;
        }

        /// <summary>
        /// Hàm thay thế nhiều khoảng trống bởi ký tự rỗng
        /// Ví dụ: "khoảng     trống" sẽ thành "khoảngtrống"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveSpace(string value)
        {
            if (value != null)
                value = Regex.Replace(value, @"\s+", string.Empty);
            return value;
        }
        /// <summary>
        /// Hàm thay thế nhiều khoảng trống bởi 1 khoảng trống
        /// Ví dụ "khoảng     trống" sẽ thành "khoảng trống"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StandardSpace(string value)
        {
            if (value != null)
                value = Regex.Replace(value, @"\s+", " ");
            if (value != null)
                value = value.TrimEnd().TrimStart();
            return value;
        }
        /// <summary>
        /// Hàm trích ra 1 chuỗi ký tự con từ chuỗi ký tự gốc, tính từ ký tự đầu tiên, có độ dài length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return value.Substring(0, length > value.Length ? value.Length : length);
        }
        public static string RemoveHtmlTags(string value, int length = 0)
        {
            if (string.IsNullOrEmpty(value)) return "";
            string result = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            result = Regex.Replace(result, @"\s{2,}", ". ");
            if (length != 0)
                result = SubString(result, length);
            return result;
        }
        /// <summary>
        /// Hàm kiểm tra xem email có đúng định dạng hay không
        /// </summary>
        /// <param name="email">Email được kiểm tra</param>
        /// <returns>trả về true nếu đúng định dạng, false nếu không đúng</returns>
        public static bool IsValidEmail(string email)
        {
            bool result = false;
            if (String.IsNullOrEmpty(email))
                return result;
            email = email.Trim();
            result = Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return result;
        }

        /// <summary>
        /// Gets query string value by name
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static string QueryString(string name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request.QueryString[name] != null)
                result = HttpContext.Current.Request.QueryString[name].ToString();
            return result;
        }

        /// <summary>
        /// Gets boolean value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static bool QueryStringBool(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            return (resultStr == "YES" || resultStr == "TRUE" || resultStr == "1");
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            int result;
            Int32.TryParse(resultStr, out result);
            return result;
        }

        /// <summary>
        /// Gets integer value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Query string value</returns>
        public static int QueryStringInt(string name, int defaultValue)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            if (resultStr.Length > 0)
            {
                return Int32.Parse(resultStr);
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets GUID value from query string 
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Query string value</returns>
        public static Guid? QueryStringGuid(string name)
        {
            string resultStr = QueryString(name).ToUpperInvariant();
            Guid? result = null;
            try
            {
                result = new Guid(resultStr);
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// Gets Form String
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Result</returns>
        public static string GetFormString(string name)
        {
            string result = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request[name] != null)
                result = HttpContext.Current.Request[name].ToString();
            return result;
        }



        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public static string ServerVariables(string name)
        {
            string tmpS = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables[name] != null)
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[name].ToString();
                }
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }



        /// <summary>
        /// Disable browser cache
        /// </summary>
        public static void DisableBrowserCache()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Cache.SetExpires(new DateTime(1995, 5, 6, 12, 0, 0, DateTimeKind.Utc));
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.AppendCacheExtension("post-check=0,pre-check=0");

            }
        }


        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public static bool IsCurrentConnectionSecured()
        {
            bool useSSL = false;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                useSSL = HttpContext.Current.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = HttpContext.Current.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSSL;
        }

        /// <summary>
        /// Gets this page name
        /// </summary>
        /// <returns></returns>
        public static string GetThisPageUrl(bool includeQueryString)
        {
            string URL = string.Empty;
            if (HttpContext.Current == null)
                return URL;

            if (includeQueryString)
            {
                bool useSSL = IsCurrentConnectionSecured();
                string storeHost = GetStoreHost(useSSL);
                if (storeHost.EndsWith("/"))
                    storeHost = storeHost.Substring(0, storeHost.Length - 1);
                URL = storeHost + HttpContext.Current.Request.RawUrl;
            }
            else
            {
                URL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path);
            }
            URL = URL.ToLowerInvariant();
            return URL;
        }

        /// <summary>
        /// Gets store location
        /// </summary>
        /// <returns>Store location</returns>
        public static string GetStoreLocation()
        {
            bool useSSL = IsCurrentConnectionSecured();
            return GetStoreLocation(useSSL);
        }

        /// <summary>
        /// Gets store location
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        /// <returns>Store location</returns>
        public static string GetStoreLocation(bool useSsl)
        {
            string result = GetStoreHost(useSsl);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            result = result + HttpContext.Current.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets store admin location
        /// </summary>
        /// <returns>Store admin location</returns>
        public static string GetStoreAdminLocation()
        {
            bool useSSL = IsCurrentConnectionSecured();
            return GetStoreAdminLocation(useSSL);
        }

        /// <summary>
        /// Gets store admin location
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        /// <returns>Store admin location</returns>
        public static string GetStoreAdminLocation(bool useSsl)
        {
            string result = GetStoreLocation(useSsl);
            result += "Administration/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Gets store host location
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        /// <returns>Store host location</returns>
        public static string GetStoreHost(bool useSsl)
        {
            string result = "http://" + ServerVariables("HTTP_HOST");
            if (!result.EndsWith("/"))
                result += "/";
            if (useSsl)
            {
                //shared SSL certificate URL
                string sharedSslUrl = string.Empty;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                    sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();

                if (!String.IsNullOrEmpty(sharedSslUrl))
                {
                    //shared SSL
                    result = sharedSslUrl;
                }
                else
                {
                    //standard SSL
                    result = result.Replace("http:/", "https:/");
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"])
                    && Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]))
                {
                    //SSL is enabled

                    //get shared SSL certificate URL
                    string sharedSslUrl = string.Empty;
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["SharedSSLUrl"]))
                        sharedSslUrl = ConfigurationManager.AppSettings["SharedSSLUrl"].Trim();
                    if (!String.IsNullOrEmpty(sharedSslUrl))
                    {
                        //shared SSL

                        /* we need to set a store URL here (IoC.Resolve<ISettingManager>().StoreUrl property)
                         * but we cannot reference Nop.BusinessLogic.dll assembly.
                         * So we are using one more app config settings - <add key="NonSharedSSLUrl" value="http://www.yourStore.com" />
                         */
                        string nonSharedSslUrl = string.Empty;
                        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["NonSharedSSLUrl"]))
                            nonSharedSslUrl = ConfigurationManager.AppSettings["NonSharedSSLUrl"].Trim();
                        if (string.IsNullOrEmpty(nonSharedSslUrl))
                            throw new Exception("NonSharedSSLUrl app config setting is not empty");
                        result = nonSharedSslUrl;
                    }
                }
            }

            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        public static void ReloadCurrentPage()
        {
            bool useSSL = IsCurrentConnectionSecured();
            ReloadCurrentPage(useSSL);
        }

        /// <summary>
        /// Reloads current page
        /// </summary>
        /// <param name="useSsl">Use SSL</param>
        public static void ReloadCurrentPage(bool useSsl)
        {
            string storeHost = GetStoreHost(useSsl);
            if (storeHost.EndsWith("/"))
                storeHost = storeHost.Substring(0, storeHost.Length - 1);
            string url = storeHost + HttpContext.Current.Request.RawUrl;
            url = url.ToLowerInvariant();
            HttpContext.Current.Response.Redirect(url);
        }

        /// <summary>
        /// Modifies query string
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryStringModification">Query string modification</param>
        /// <param name="targetLocationModification">Target location modification</param>
        /// <returns>New url</returns>
        public static string ModifyQueryString(string url, string queryStringModification, string targetLocationModification)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (targetLocationModification == null)
                targetLocationModification = string.Empty;
            targetLocationModification = targetLocationModification.ToLowerInvariant();


            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new char[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(targetLocationModification))
            {
                str2 = targetLocationModification;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }

        /// <summary>
        /// Remove query string from url
        /// </summary>
        /// <param name="url">Url to modify</param>
        /// <param name="queryString">Query string to remove</param>
        /// <returns>New url</returns>
        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();


            string str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryString))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    dictionary.Remove(queryString);

                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }

        /// <summary>
        /// Ensures that requested page is secured (https://)
        /// </summary>
        public static void EnsureSsl()
        {
            if (!IsCurrentConnectionSecured())
            {
                bool useSSL = false;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["UseSSL"]))
                    useSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]);
                if (useSSL)
                {
                    //if (!HttpContext.Current.Request.Url.IsLoopback)
                    //{
                    ReloadCurrentPage(true);
                    //}
                }
            }
        }

        /// <summary>
        /// Ensures that requested page is not secured (http://)
        /// </summary>
        public static void EnsureNonSsl()
        {
            if (IsCurrentConnectionSecured())
            {
                ReloadCurrentPage(false);
            }
        }

        /// <summary>
        /// Sets cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="cookieValue">Cookie value</param>
        /// <param name="ts">Timespan</param>
        public static void SetCookie(string cookieName, string cookieValue, TimeSpan ts)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = HttpContext.Current.Server.UrlEncode(cookieValue);
                DateTime dt = DateTime.Now;
                cookie.Expires = dt.Add(ts);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Gets cookie string
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <param name="decode">Decode cookie</param>
        /// <returns>Cookie string</returns>
        public static string GetCookieString(string cookieName, bool decode)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] == null)
            {
                return string.Empty;
            }
            try
            {
                string tmp = HttpContext.Current.Request.Cookies[cookieName].Value.ToString();
                if (decode)
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                return tmp;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets boolean value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public static bool GetCookieBool(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true).ToUpperInvariant();
            return (str1 == "TRUE" || str1 == "YES" || str1 == "1");
        }

        /// <summary>
        /// Gets integer value from cookie
        /// </summary>
        /// <param name="cookieName">Cookie name</param>
        /// <returns>Result</returns>
        public static int GetCookieInt(string cookieName)
        {
            string str1 = GetCookieString(cookieName, true);
            if (!String.IsNullOrEmpty(str1))
                return Convert.ToInt32(str1);
            else
                return 0;
        }

        /// <summary>
        /// Gets boolean value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Result</returns>
        public static bool ConfigGetBooleanValue(NameValueCollection config,
            string valueName, bool defaultValue)
        {
            bool result;
            string str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!bool.TryParse(str1, out result))
                throw new Exception(string.Format("Value must be boolean {0}", valueName));
            return result;
        }

        /// <summary>
        /// Gets integer value from NameValue collection
        /// </summary>
        /// <param name="config">NameValue collection</param>
        /// <param name="valueName">Name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="zeroAllowed">Zero allowed</param>
        /// <param name="maxValueAllowed">Max value allowed</param>
        /// <returns>Result</returns>
        public static int ConfigGetIntValue(NameValueCollection config,
            string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            int result;
            string str1 = config[valueName];
            if (str1 == null)
                return defaultValue;
            if (!int.TryParse(str1, out result))
            {
                if (zeroAllowed)
                {
                    throw new Exception(string.Format("Value must be non negative integer {0}", valueName));
                }
                throw new Exception(string.Format("Value must be positive integer {0}", valueName));
            }
            if (zeroAllowed && (result < 0))
                throw new Exception(string.Format("Value must be non negative integer {0}", valueName));
            if (!zeroAllowed && (result <= 0))
                throw new Exception(string.Format("Value must be positive integer {0}", valueName));
            if ((maxValueAllowed > 0) && (result > maxValueAllowed))
                throw new Exception(string.Format("Value too big {0}", valueName));
            return result;
        }

        /// <summary>
        /// Write XML to response
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseXml(string xml, string fileName)
        {
            if (!String.IsNullOrEmpty(xml))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlDeclaration decl = document.FirstChild as XmlDeclaration;
                if (decl != null)
                {
                    decl.Encoding = "utf-8";
                }
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xml";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                response.BinaryWrite(Encoding.UTF8.GetBytes(document.InnerXml));
                response.End();
            }
        }

        /// <summary>
        /// Write text to response
        /// </summary>
        /// <param name="txt">text</param>
        /// <param name="fileName">Filename</param>
        public static void WriteResponseTxt(string txt, string fileName)
        {
            if (!String.IsNullOrEmpty(txt))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/plain";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
                response.BinaryWrite(Encoding.UTF8.GetBytes(txt));
                response.End();
            }
        }

        /// <summary>
        /// Write XLS file to response
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="targetFileName">Target file name</param>
        public static void WriteResponseXls(string filePath, string targetFileName)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/xls";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Write PDF file to response
        /// </summary>
        /// <param name="filePath">File napathme</param>
        /// <param name="targetFileName">Target file name</param>
        /// <remarks>For BeatyStore project</remarks>
        public static void WriteResponsePdf(string filePath, string targetFileName)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Charset = "utf-8";
                response.ContentType = "text/pdf";
                response.AddHeader("content-disposition", string.Format("attachment; filename={0}", targetFileName));
                response.BinaryWrite(File.ReadAllBytes(filePath));
                response.End();
            }
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }

        /// <summary>
        /// Fills drop down list with values of enumaration
        /// </summary>
        /// <param name="list">Dropdownlist</param>
        /// <param name="enumType">Enumeration</param>
        public static void FillDropDownWithEnum(DropDownList list, Type enumType)
        {
            FillDropDownWithEnum(list, enumType, true);
        }

        /// <summary>
        /// Fills drop down list with values of enumaration
        /// </summary>
        /// <param name="list">Dropdownlist</param>
        /// <param name="enumType">Enumeration</param>
        /// <param name="clearListItems">Clear list of exsisting items</param>
        public static void FillDropDownWithEnum(DropDownList list, Type enumType, bool clearListItems)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumType must be enum type");
            }

            if (clearListItems)
            {
                list.Items.Clear();
            }
            string[] strArray = Enum.GetNames(enumType);
            foreach (string str2 in strArray)
            {
                int enumValue = (int)Enum.Parse(enumType, str2, true);
                ListItem ddlItem = new ListItem(CommonHelper.ConvertEnum(str2), enumValue.ToString());
                list.Items.Add(ddlItem);
            }
        }

        /// <summary>
        /// Set response NoCache
        /// </summary>
        /// <param name="response">Response</param>
        public static void SetResponseNoCache(HttpResponse response)
        {
            if (response == null)
                throw new ArgumentNullException("response");

            //response.Cache.SetCacheability(HttpCacheability.NoCache) 

            response.CacheControl = "private";
            response.Expires = 0;
            response.AddHeader("pragma", "no-cache");
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
                return str.Substring(0, maxLength);
            else
                return str;
        }

        /// <summary>
        /// Ensures that a string only contains numeric values
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();

            // Loop is faster than RegEx
            //return Regex.Replace(str, "\\D", "");
        }

        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Get a value indicating whether content page is requested
        /// </summary>
        /// <returns>Result</returns>
        public static bool IsContentPageRequested()
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            if (!request.Url.LocalPath.ToLower().EndsWith(".aspx") &&
                !request.Url.LocalPath.ToLower().EndsWith(".asmx") &&
                !request.Url.LocalPath.ToLower().EndsWith(".ashx"))
            {
                return false;
            }

            return true;
        }

        #endregion
        #region Glocal
        public static string CurrencyFormat(decimal value)
        {
            return value.ToString("0,0.##");
        }
        public static string UrlFormat(string extend)
        {
            string result = extend;
            return result;
        }

        public static string ToImageUrl(string cdn, string url, string type)
        {
            if (string.Equals(type, ".doc", StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(type, ".docx", StringComparison.CurrentCultureIgnoreCase))
                return "/Content/file_manager/file_doc.png";

            if (string.Equals(type, ".pdf", StringComparison.CurrentCultureIgnoreCase))
                return "/Content/file_manager/file_pdf.png";

            if (string.Equals(type, ".xls", StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(type, ".xlsx", StringComparison.CurrentCultureIgnoreCase))
                return "/Content/file_manager/excel.png";

            return cdn + url;
        }
        public static string MoneyFormat(string value)
        {
            return String.Format("{0:C}", value).Replace("$", string.Empty);
        }

        public static string FormatInjection(string value)
        {
            value = value.Replace("'", "");
            value = value.Replace("<", "");
            value = value.Replace(">", "");
            value = value.Replace("script", "");
            value = value.Replace("1=1", "");
            value = value.Replace("\\", "");
            value = value.Replace("/", "");
            return value;
        }

        private static readonly string[] VietnameseSigns = new string[]
                                                               {

                                                                   "aAeEoOuUiIdDyY_",

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

                                                                   "ÝỲỴỶỸ",
                                                                   " "
                                                               };

        public static bool IsSpecialChar(string content)
        {
            return Regex.IsMatch(content, "^[a-zA-Z0-9_]+$");
        }

        public static bool IsNumber(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return false;

            return Regex.IsMatch(content, "^[0-9.]+$");
        }

        public static bool IsInteger(string sInput)
        {
            return Regex.IsMatch(sInput, "^\\d+(\\\\d+)?$");
        }

        public static bool IsVietnames(string sInput)
        {
            /*
             *  Các kí tự từ A->Z ; a->z.
                Không số
                Không kí tự đặc biệt
                Và Các kí tự tiếng Việt Nam
             */
            return Regex.IsMatch(sInput, "^[a-zA-Z_ ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵýỷỹ-]+$");
        }

        public static string NullToEmpty(Object input)
        {
            return (input == null ? "" : ("null".Equals(input) ? "" : input.ToString()));
        }

        public static string ConvertToUnSign1(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (char t in stFormD)
            {
                UnicodeCategory uc = /*System.Globalization.*/CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static string ConvertToUnSign(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var temp = input.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveSign4VietnameseString(string str, string regex, string spaceReplaceWith)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]).Replace(" ", spaceReplaceWith);
            }
            str = RemoveSpecialChar(str, regex);
            return str;
        }

        public static int ToInt(string value)
        {
            if (IsNumber(value))
                return int.Parse(value);
            return 0;
        }

        public static short? ToShort(string value)
        {
            if (IsNumber(value))
                return (short?)int.Parse(value);
            return null;
        }

        public static string ToString(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }

        public static int ToInt(string value, int input)
        {
            if (IsNumber(value))
                return int.Parse(value);
            return input;
        }
        public static Guid ToGuid(string value)
        {
            Guid tempStaffId;
            Guid.TryParse(value, out tempStaffId);
            return tempStaffId;
        }
        public static bool? ToBool(string value)
        {
            bool outValue;
            if (bool.TryParse(value, out outValue))
                return outValue;
            return null;
        }
        public static string RemoveSpecialChar(string content)
        {
            return Regex.Replace(content, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static string RemoveSpecialChar(string content, string regex)
        {
            return Regex.Replace(content, regex, "", RegexOptions.Compiled);
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]).Replace(" ", "_");
            }
            //str = str.Replace(":", "");
            //str = str.Replace("/", "");
            //str = str.Replace("'", "");
            //str = str.Replace('"', "");
            //str = str.Replace("~", "");
            //str = str.Replace("?", "");
            //str = str.Replace("!", "");
            //str = str.Replace("@", "");
            //str = str.Replace("#", "");
            //str = str.Replace("$", "");
            //str = str.Replace("%", "");
            //str = str.Replace("(", "");
            str = str.Replace("%20", "");
            str = RemoveSpecialChar(str);
            return str;
        }

        public static string DecimalToUTF8(string content)
        {
            var charCode = new string[] { "&#224;", "&#225;", "&#226;", "&#227;", "&#242;", "&#243;", "&#244;", "&#245;", "&#232;", "&#233;", "&#234;", "&#236;", "&#237;", "&#249;", "&#250;" };
            var charNew = new string[] { "à", "á", "â", "ã", "ò", "ó", "ô", "õ", "è", "é", "ê", "ì", "í", "ù", "ú" };
            int index = 0;
            foreach (string chars in charCode)
            {
                content = content.Replace(chars, charNew[index]);
                index++;
            }

            return content;
        }

        public static bool SendMail(MailMessage message, string username, string password)
        {
            try
            {
                // Get config send mail
                var smtpClient = new SmtpClient
                {
                    Host =
                        ConfigurationManager.AppSettings["MailHost"].ToString(
                            CultureInfo.InvariantCulture),
                    Port =
                        int.Parse(
                            ConfigurationManager.AppSettings["MailPost"].ToString(
                                CultureInfo.InvariantCulture)),
                    EnableSsl = false
                };


                var myCreds = new NetworkCredential(username, password);
                smtpClient.Credentials = myCreds;

                // Send SMTP mail
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                //LogFile(ex.Message + " - source: " + ex.Source);
                return false;
            }

            return true;
        }

        public static int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string ReadFile(string filePath)
        {
            //filePath = HttpContext.Current.Server.MapPath(filePath);
            // Read the file lines into a string array.
            string[] lines = System.IO.File.ReadAllLines(filePath);

            return lines.Aggregate(string.Empty, (current, line) => current + line);
        }


        public static string CheckDate(string dateInput)
        {
            string dateOutput = "";
            if (!string.IsNullOrEmpty(dateInput))
            {
                dateOutput = dateInput == "1/1/1900 12:00:00 AM" ? "" : dateInput;
            }

            return dateOutput;
        }

        /// <summary>
        /// Format show date for news, recruitment: dd/MM/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatShowDate(string date)
        {
            string result = "";
            try
            {
                if (!string.IsNullOrEmpty(date))
                {
                    IFormatProvider culture = new CultureInfo("fr-FR", true);
                    DateTime dateConvert = DateTime.Parse(date);
                    result = dateConvert.ToString("dd/MM/yyyy");
                }
            }
            catch { }
            return result;
        }

        /// <summary>
        /// Replace all space in text
        /// </summary>
        public static Regex MultipleSpaces = new Regex(@"\s+", RegexOptions.Compiled);
        private static bool IsPrivateIpAddress(string ipAddress)
        {
            // http://en.wikipedia.org/wiki/Private_network
            // Private IP Addresses are: 
            //  24-bit block: 10.0.0.0 through 10.255.255.255
            //  20-bit block: 172.16.0.0 through 172.31.255.255
            //  16-bit block: 192.168.0.0 through 192.168.255.255
            //  Link-local addresses: 169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)

            var ip = System.Net.IPAddress.Parse(ipAddress);
            var octets = ip.GetAddressBytes();

            var is24BitBlock = octets[0] == 10;
            if (is24BitBlock) return true; // Return to prevent further processing

            var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
            if (is20BitBlock) return true; // Return to prevent further processing

            var is16BitBlock = octets[0] == 192 && octets[1] == 168;
            if (is16BitBlock) return true; // Return to prevent further processing

            var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
            return isLinkLocalAddress;
        }
        public static string GetIPAddress(HttpRequestBase request)
        {
            string szRemoteAddr = request.UserHostAddress;
            string szXForwardedFor = request.ServerVariables["X_FORWARDED_FOR"];
            string szIP = "";

            if (szXForwardedFor == null)
            {
                szIP = szRemoteAddr;
            }
            else
            {
                szIP = szXForwardedFor;
                if (szIP.IndexOf(",") > 0)
                {
                    string[] arIPs = szIP.Split(',');

                    foreach (string item in arIPs)
                    {
                        if (!IsPrivateIpAddress(item))
                        {
                            return item;
                        }
                    }
                }
            }
            return szIP;
        }
        #endregion
    }
}
