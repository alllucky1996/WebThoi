using System;
using System.Text;
using System.Web.Mvc;
using Common.Paging;

namespace Web.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PagingInfo pagingInfo,
        Func<int, string> pageUrl)
        {
            StringBuilder result0 = new StringBuilder();
            StringBuilder result1 = new StringBuilder();
            TagBuilder tag0 = new TagBuilder("ol");
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag1 = new TagBuilder("li");
                if (i == pagingInfo.CurrentPage)
                    tag1.AddCssClass("current");

                TagBuilder tag2 = new TagBuilder("a");
                tag2.MergeAttribute("href", pageUrl(i));
                tag2.InnerHtml = i.ToString();

                tag1.InnerHtml=tag2.ToString();
                tag0.InnerHtml += tag1.ToString();
                //result0.Append(tag1.ToString());
            }
            //tag0.SetInnerText(result0.ToString());
            result1.Append(tag0.ToString());
            return MvcHtmlString.Create(result1.ToString());
        }
    }
}