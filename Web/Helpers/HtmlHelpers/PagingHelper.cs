using System;
using System.Text;
using System.Web.Mvc;

namespace Web.HtmlHelpers
{
    public static class PagingHelper
    {
        private static string buildLinks(int start, int end, string innerContent,
        Func<int, string> pageUrl, int currentPage)
        {
            StringBuilder result = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == currentPage)
                {
                    result.Append("<span class='current'>" + (innerContent ?? i.ToString()) + "</span>");
                }
                else
                {
                    result.Append(" <a class=\"" + (i == currentPage ? "" : "inactive") + "\" href=\"" + pageUrl(i) + "\">" + (innerContent ?? i.ToString()) + "</a> ");
                }
            }
            return result.ToString();
        }

        public static string pageLinks(this HtmlHelper html, int totalPage, int currentPage,
        Func<int, string> pageUrl)
        {
            const int maxPages = 10;
            StringBuilder result = new StringBuilder();
            if (totalPage <= maxPages)
            {
                return buildLinks(1, totalPage, null, pageUrl, currentPage);
            }
            int pagesAfter = totalPage - currentPage; // Number of pages after current
            int pagesBefore = currentPage - 1; // Number of pages before current
            if (pagesAfter <= 4)
            {
                result.Append(buildLinks(1, 1, null, pageUrl, currentPage));
                int pageSubset = totalPage - maxPages - 1 > 1 ? totalPage - maxPages - 1 : 2;
                result.Append(buildLinks(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(buildLinks(totalPage - maxPages + 3, totalPage, null, pageUrl, currentPage)); // Show last pages
                return result.ToString(); // Exit
            }
            if (pagesBefore <= 4)
            {
                result.Append(buildLinks(1, maxPages - 2, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset = maxPages + 2 < totalPage ? maxPages + 2 : totalPage - 1;
                result.Append(buildLinks(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(buildLinks(totalPage, totalPage, null, pageUrl, currentPage));// Show last page
                return result.ToString(); // Exit
            }
            if (pagesAfter > 4)
            {
                result.Append(buildLinks(1, 1, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                int pageSubset2 = currentPage + 7 < totalPage ? currentPage + 7 : totalPage - 1;
                result.Append(buildLinks(pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...", pageUrl, currentPage)); // Show 1st page subset (...)
                result.Append(buildLinks(currentPage - 3, currentPage + 3, null, pageUrl, currentPage)); // Show middle pages
                // Show 2nd page subset (...)
                // only show ... if page is contigous to the previous one.
                result.Append(buildLinks(pageSubset2, pageSubset2, pageSubset2 == currentPage + 4 ? null : "...", pageUrl, currentPage));
                result.Append(buildLinks(totalPage, totalPage, null, pageUrl, currentPage)); // Show last page
                return result.ToString(); // Exit
            }
            return result.ToString(); // Exit
        }

        //Hàm dùng trong trang quản trị
        private static string buildLinks2(int start, int end, string innerContent, int currentPage)
        {
            StringBuilder result = new StringBuilder();
            for (int i = start; i <= end; i++)
            {                
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", "#");
                tag.AddCssClass("next-article-page");
                tag.MergeAttribute("data-page", i.ToString());
                tag.InnerHtml = (innerContent ?? i.ToString());
                if (i == currentPage)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }
            return result.ToString();
        }

        public static string pageLinks2(this HtmlHelper html, int totalPage, int currentPage)
        {
            const int maxPages = 10;
            StringBuilder result = new StringBuilder();
            if (totalPage <= maxPages)
            {
                return buildLinks2(1, totalPage, null,  currentPage);
            }
            int pagesAfter = totalPage - currentPage; // Number of pages after current
            int pagesBefore = currentPage - 1; // Number of pages before current
            if (pagesAfter <= 4)
            {
                result.Append(buildLinks2(1, 1, null,  currentPage));
                int pageSubset = totalPage - maxPages - 1 > 1 ? totalPage - maxPages - 1 : 2;
                result.Append(buildLinks2(pageSubset, pageSubset, "...",  currentPage)); // Show page subset (...)
                result.Append(buildLinks2(totalPage - maxPages + 3, totalPage, null,  currentPage)); // Show last pages
                return result.ToString(); // Exit
            }
            if (pagesBefore <= 4)
            {
                result.Append(buildLinks2(1, maxPages - 2, null,  currentPage)); // Show 1st pages
                int pageSubset = maxPages + 2 < totalPage ? maxPages + 2 : totalPage - 1;
                result.Append(buildLinks2(pageSubset, pageSubset, "...",  currentPage)); // Show page subset (...)
                result.Append(buildLinks2(totalPage, totalPage, null,  currentPage));// Show last page
                return result.ToString(); // Exit
            }
            if (pagesAfter > 4)
            {
                result.Append(buildLinks2(1, 1, null,  currentPage)); // Show 1st pages
                int pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                int pageSubset2 = currentPage + 7 < totalPage ? currentPage + 7 : totalPage - 1;
                result.Append(buildLinks2(pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...",  currentPage)); // Show 1st page subset (...)
                result.Append(buildLinks2(currentPage - 3, currentPage + 3, null,  currentPage)); // Show middle pages
                // Show 2nd page subset (...)
                // only show ... if page is contigous to the previous one.
                result.Append(buildLinks2(pageSubset2, pageSubset2, pageSubset2 == currentPage + 4 ? null : "...",  currentPage));
                result.Append(buildLinks2(totalPage, totalPage, null,  currentPage)); // Show last page
                return result.ToString(); // Exit
            }
            return result.ToString(); // Exit
        }

        //Hàm dùng trong trang quản trị
        private static string buildLinks3(int start, int end, string innerContent, int currentPage)
        {
            StringBuilder result = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", "#");
                tag.AddCssClass("next-article-page-2");
                tag.MergeAttribute("data-page", i.ToString());
                tag.InnerHtml = (innerContent ?? i.ToString());
                if (i == currentPage)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }
            return result.ToString();
        }

        public static string pageLinks3(this HtmlHelper html, int totalPage, int currentPage)
        {
            const int maxPages = 10;
            StringBuilder result = new StringBuilder();
            if (totalPage <= maxPages)
            {
                return buildLinks3(1, totalPage, null, currentPage);
            }
            int pagesAfter = totalPage - currentPage; // Number of pages after current
            int pagesBefore = currentPage - 1; // Number of pages before current
            if (pagesAfter <= 4)
            {
                result.Append(buildLinks3(1, 1, null, currentPage));
                int pageSubset = totalPage - maxPages - 1 > 1 ? totalPage - maxPages - 1 : 2;
                result.Append(buildLinks3(pageSubset, pageSubset, "...", currentPage)); // Show page subset (...)
                result.Append(buildLinks3(totalPage - maxPages + 3, totalPage, null, currentPage)); // Show last pages
                return result.ToString(); // Exit
            }
            if (pagesBefore <= 4)
            {
                result.Append(buildLinks3(1, maxPages - 2, null, currentPage)); // Show 1st pages
                int pageSubset = maxPages + 2 < totalPage ? maxPages + 2 : totalPage - 1;
                result.Append(buildLinks3(pageSubset, pageSubset, "...", currentPage)); // Show page subset (...)
                result.Append(buildLinks3(totalPage, totalPage, null, currentPage));// Show last page
                return result.ToString(); // Exit
            }
            if (pagesAfter > 4)
            {
                result.Append(buildLinks3(1, 1, null, currentPage)); // Show 1st pages
                int pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                int pageSubset2 = currentPage + 7 < totalPage ? currentPage + 7 : totalPage - 1;
                result.Append(buildLinks3(pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...", currentPage)); // Show 1st page subset (...)
                result.Append(buildLinks3(currentPage - 3, currentPage + 3, null, currentPage)); // Show middle pages
                // Show 2nd page subset (...)
                // only show ... if page is contigous to the previous one.
                result.Append(buildLinks3(pageSubset2, pageSubset2, pageSubset2 == currentPage + 4 ? null : "...", currentPage));
                result.Append(buildLinks3(totalPage, totalPage, null, currentPage)); // Show last page
                return result.ToString(); // Exit
            }
            return result.ToString(); // Exit
        }
        private static string PhanTrangCongThongTin1(int start, int end, string innerContent,
        Func<int, string> pageUrl, int currentPage)
        {
            StringBuilder result = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == currentPage)
                {
                    result.Append("<a href='javascript:;' class='current'>" + (innerContent ?? i.ToString()) + "</a></li>");
                }
                else
                {
                    result.Append("<a href=\"" + pageUrl(i) + "\">" + (innerContent ?? i.ToString()) + "</a>");
                }
            }
            return result.ToString();
        }
        public static string PhanTrangCongThongTin(this HtmlHelper html, int totalPage, int currentPage,
        Func<int, string> pageUrl)
        {
            const int maxPages = 10;
            StringBuilder result = new StringBuilder();
            if (totalPage <= maxPages)
            {
                return PhanTrangCongThongTin1(1, totalPage, null, pageUrl, currentPage);
            }
            int pagesAfter = totalPage - currentPage; // Number of pages after current
            int pagesBefore = currentPage - 1; // Number of pages before current
            if (pagesAfter <= 4)
            {
                result.Append(PhanTrangCongThongTin1(1, 1, null, pageUrl, currentPage));
                int pageSubset = totalPage - maxPages - 1 > 1 ? totalPage - maxPages - 1 : 2;
                result.Append(PhanTrangCongThongTin1(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(PhanTrangCongThongTin1(totalPage - maxPages + 3, totalPage, null, pageUrl, currentPage)); // Show last pages
                return result.ToString(); // Exit
            }
            if (pagesBefore <= 4)
            {
                result.Append(PhanTrangCongThongTin1(1, maxPages - 2, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset = maxPages + 2 < totalPage ? maxPages + 2 : totalPage - 1;
                result.Append(PhanTrangCongThongTin1(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(PhanTrangCongThongTin1(totalPage, totalPage, null, pageUrl, currentPage));// Show last page
                return result.ToString(); // Exit
            }
            if (pagesAfter > 4)
            {
                result.Append(PhanTrangCongThongTin1(1, 1, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                int pageSubset2 = currentPage + 7 < totalPage ? currentPage + 7 : totalPage - 1;
                result.Append(PhanTrangCongThongTin1(pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...", pageUrl, currentPage)); // Show 1st page subset (...)
                result.Append(PhanTrangCongThongTin1(currentPage - 3, currentPage + 3, null, pageUrl, currentPage)); // Show middle pages
                // Show 2nd page subset (...)
                // only show ... if page is contigous to the previous one.
                result.Append(PhanTrangCongThongTin1(pageSubset2, pageSubset2, pageSubset2 == currentPage + 4 ? null : "...", pageUrl, currentPage));
                result.Append(PhanTrangCongThongTin1(totalPage, totalPage, null, pageUrl, currentPage)); // Show last page
                return result.ToString(); // Exit
            }
            return result.ToString(); // Exit
        }
        private static string PhanTrangSanGiaoDich1(int start, int end, string innerContent,
       Func<int, string> pageUrl, int currentPage)
        {
            StringBuilder result = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == currentPage)
                {
                    result.Append("<a href='javascript:;' class='current'>" + (innerContent ?? i.ToString()) + "</a>");
                }
                else
                {
                    result.Append("<a href=\"" + pageUrl(i) + "\">" + (innerContent ?? i.ToString()) + "</a>");
                }
            }
            return result.ToString();
        }
        public static string PhanTrangSanGiaoDich(this HtmlHelper html, int totalPage, int currentPage,
        Func<int, string> pageUrl)
        {
            const int maxPages = 10;
            StringBuilder result = new StringBuilder();
            if (totalPage <= maxPages)
            {
                return PhanTrangSanGiaoDich1(1, totalPage, null, pageUrl, currentPage);
            }
            int pagesAfter = totalPage - currentPage; // Number of pages after current
            int pagesBefore = currentPage - 1; // Number of pages before current
            if (pagesAfter <= 4)
            {
                result.Append(PhanTrangSanGiaoDich1(1, 1, null, pageUrl, currentPage));
                int pageSubset = totalPage - maxPages - 1 > 1 ? totalPage - maxPages - 1 : 2;
                result.Append(PhanTrangSanGiaoDich1(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(PhanTrangSanGiaoDich1(totalPage - maxPages + 3, totalPage, null, pageUrl, currentPage)); // Show last pages
                return result.ToString(); // Exit
            }
            if (pagesBefore <= 4)
            {
                result.Append(PhanTrangSanGiaoDich1(1, maxPages - 2, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset = maxPages + 2 < totalPage ? maxPages + 2 : totalPage - 1;
                result.Append(PhanTrangSanGiaoDich1(pageSubset, pageSubset, "...", pageUrl, currentPage)); // Show page subset (...)
                result.Append(PhanTrangSanGiaoDich1(totalPage, totalPage, null, pageUrl, currentPage));// Show last page
                return result.ToString(); // Exit
            }
            if (pagesAfter > 4)
            {
                result.Append(PhanTrangSanGiaoDich1(1, 1, null, pageUrl, currentPage)); // Show 1st pages
                int pageSubset1 = currentPage - 7 > 1 ? currentPage - 7 : 2;
                int pageSubset2 = currentPage + 7 < totalPage ? currentPage + 7 : totalPage - 1;
                result.Append(PhanTrangSanGiaoDich1(pageSubset1, pageSubset1, pageSubset1 == currentPage - 4 ? null : "...", pageUrl, currentPage)); // Show 1st page subset (...)
                result.Append(PhanTrangSanGiaoDich1(currentPage - 3, currentPage + 3, null, pageUrl, currentPage)); // Show middle pages
                // Show 2nd page subset (...)
                // only show ... if page is contigous to the previous one.
                result.Append(PhanTrangSanGiaoDich1(pageSubset2, pageSubset2, pageSubset2 == currentPage + 4 ? null : "...", pageUrl, currentPage));
                result.Append(PhanTrangSanGiaoDich1(totalPage, totalPage, null, pageUrl, currentPage)); // Show last page
                return result.ToString(); // Exit
            }
            return result.ToString(); // Exit
        }
    }
}