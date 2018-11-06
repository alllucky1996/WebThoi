using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

namespace Web.Helpers
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string PageUrl { get; set; }

        private bool HasNext
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }

        private bool HasBack
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        private int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }

        public MvcHtmlString Paging()
        {
            var result = new StringBuilder();
            if (TotalPages <= 0)
                return MvcHtmlString.Create(result.ToString());

            const int detal = 3;
            const int showPage = 2 * detal + 1;
            const string page = "";

            var ulTag = new TagBuilder("ul");
            ulTag.InnerHtml += Environment.NewLine;
            ulTag.AddCssClass("paging");

            var liTag = new TagBuilder("li") { InnerHtml = string.Format("{0}-{1} tổng {2}", (CurrentPage - 1) * ItemsPerPage + 1, (CurrentPage - 1) * ItemsPerPage + (this.ItemsPerPage < this.TotalItems ? this.ItemsPerPage : this.TotalItems), this.TotalItems) };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag.ToString(), Environment.NewLine);

            TagBuilder tag;
            tag = new TagBuilder("a");
            tag.MergeAttribute("href", this.PageUrl + page + 1);
            tag.AddCssClass("prev");
            tag.InnerHtml = "<<";
            result.Append(tag.ToString());

            if (this.TotalPages <= showPage)
                for (var i = 1; i <= this.TotalPages; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", this.PageUrl + page + i);
                    tag.InnerHtml = i.ToString();
                    tag.MergeAttribute("class", i == this.CurrentPage ? "active" : string.Empty);
                    result.Append(tag.ToString());
                }
            else
            {
                var start = 1;
                var end = this.TotalPages;
                var current = this.CurrentPage;

                if (current + detal < end)
                    end = current + detal;

                if (current - detal >= start)
                    start = current - detal;

                if (this.HasBack)
                {
                    tag = new TagBuilder("a") { InnerHtml = "..." };
                    result.Append(tag.ToString());
                }

                for (var i = start; i <= end; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", this.PageUrl + page + i);
                    tag.InnerHtml = i.ToString();
                    tag.MergeAttribute("class", i == this.CurrentPage ? "active" : string.Empty);
                    result.Append(tag.ToString());
                }
                if (this.HasNext)
                {
                    tag = new TagBuilder("a") { InnerHtml = "..." };
                    result.Append(tag.ToString());
                }
            }

            tag = new TagBuilder("a");
            tag.MergeAttribute("href", this.PageUrl + page + this.TotalPages);
            tag.InnerHtml = ">>";
            tag.AddCssClass("next");
            result.Append(tag.ToString());

            liTag = new TagBuilder("li") { InnerHtml = result.ToString() };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag, Environment.NewLine);

            return MvcHtmlString.Create(ulTag.ToString());
        }

        public MvcHtmlString Paging1()
        {

            var result = new StringBuilder();
            if (TotalPages <= 0)
                return MvcHtmlString.Create(result.ToString());

            const int detal = 3;
            const int showPage = 2 * detal + 1;
            const string page = "";

            var ulTag = new TagBuilder("ul");
            ulTag.InnerHtml += Environment.NewLine;
            ulTag.AddCssClass("paging");

            var liTag = new TagBuilder("li") { InnerHtml = string.Format("<font style='font-size: 11px;'>Hiển thị từ {0} đến {1} kết quả - tổng số {2} kết quả</font>&nbsp;", (CurrentPage - 1) * ItemsPerPage + 1, (CurrentPage - 1) * ItemsPerPage + (this.ItemsPerPage < this.TotalItems ? this.ItemsPerPage : this.TotalItems), this.TotalItems) };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag.ToString(), Environment.NewLine);

            TagBuilder tag;
            tag = new TagBuilder("a");
            tag.MergeAttribute("href", this.PageUrl + page + 1);
            tag.AddCssClass("prev");
            tag.MergeAttribute("style", "float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
            tag.InnerHtml = "<<";
            result.Append(tag.ToString());

            if (this.TotalPages <= showPage)
                for (var i = 1; i <= this.TotalPages; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", this.PageUrl + page + i);
                    tag.InnerHtml = i.ToString();
                    tag.MergeAttribute("style", i == this.CurrentPage ? "float: left; width: 20px; border: 1px solid gray;text-align:center;color:red;font-size:11px;font-weight:bold;margin-top: 3px;margin-right: 1px;" : "float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
                    result.Append(tag.ToString());
                }
            else
            {
                var start = 1;
                var end = this.TotalPages;
                var current = this.CurrentPage;

                if (current + detal < end)
                    end = current + detal;

                if (current - detal >= start)
                    start = current - detal;

                if (this.HasBack)
                {
                    tag = new TagBuilder("a") { InnerHtml = "..." };
                    result.Append(tag.ToString());
                }

                for (var i = start; i <= end; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("href", this.PageUrl + page + i);
                    tag.InnerHtml = i.ToString();
                    tag.MergeAttribute("style", i == this.CurrentPage ? "float: left; width: 20px; border: 1px solid gray;text-align:center;color:red;font-size:11px;font-weight:bold;margin-top: 3px;margin-right: 1px;" : "float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
                    result.Append(tag.ToString());
                }
                if (this.HasNext)
                {
                    result.Append(tag.ToString());
                }
            }

            tag = new TagBuilder("a");
            tag.MergeAttribute("href", this.PageUrl + page + this.TotalPages);
            tag.InnerHtml = ">>";
            tag.MergeAttribute("style", "float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
            result.Append(tag.ToString());

            liTag = new TagBuilder("li") { InnerHtml = result.ToString() };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag, Environment.NewLine);

            return MvcHtmlString.Create(ulTag.ToString());
        }

        public MvcHtmlString PagingWithOnclick(string methodName)
        {

            var result = new StringBuilder();
            if (TotalPages <= 0)
                return MvcHtmlString.Create(result.ToString());

            const int detal = 3;
            const int showPage = 2 * detal + 1;
            const string page = "";

            var ulTag = new TagBuilder("ul");
            ulTag.InnerHtml += Environment.NewLine;
            ulTag.AddCssClass("paging");

            var liTag = new TagBuilder("li") { InnerHtml = string.Format("<font style='font-size: 11px;'>Hiển thị từ {0} đến {1} kết quả - tổng số {2} kết quả</font>&nbsp;", (CurrentPage - 1) * ItemsPerPage + 1, (CurrentPage - 1) * ItemsPerPage + (this.ItemsPerPage < this.TotalItems ? this.ItemsPerPage : this.TotalItems), this.TotalItems) };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag.ToString(), Environment.NewLine);

            TagBuilder tag;
            tag = new TagBuilder("a");
            /*-----------First Page--------------*/
            tag.MergeAttribute("onclick", methodName + "(" + (page + 1) + ")");
            tag.AddCssClass("prev");
            tag.MergeAttribute("style", "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
            tag.InnerHtml = "<<";
            result.Append(tag);

            if (this.TotalPages <= showPage)
                for (var i = 1; i <= this.TotalPages; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("onclick", methodName + "(" + (page + i) + ")");
                    tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    tag.MergeAttribute("style", i == this.CurrentPage ? "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:red;font-size:11px;font-weight:bold;margin-top: 3px;margin-right: 1px;" : "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
                    result.Append(tag);
                }
            else
            {
                var start = 1;
                var end = this.TotalPages;
                var current = this.CurrentPage;

                if (current + detal < end)
                    end = current + detal;

                if (current - detal >= start)
                    start = current - detal;

                if (this.HasBack)
                {
                    tag = new TagBuilder("a") { InnerHtml = "..." };
                    result.Append(tag);
                }

                for (var i = start; i <= end; i++)
                {
                    tag = new TagBuilder("a");
                    tag.MergeAttribute("onclick", methodName + "(" + (page + i) + ")");
                    tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    tag.MergeAttribute("style", i == this.CurrentPage ? "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:red;font-size:11px;font-weight:bold;margin-top: 3px;margin-right: 1px;" : "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
                    result.Append(tag);
                }
                if (this.HasNext)
                {
                    result.Append(tag);
                }
            }

            tag = new TagBuilder("a");
            tag.MergeAttribute("onclick", methodName + "(" + (page + this.TotalPages) + ")");
            tag.InnerHtml = ">>";
            tag.MergeAttribute("style", "cursor: pointer;float: left; width: 20px; border: 1px solid gray;text-align:center;color:black;font-size:11px;margin-top: 3px;margin-right: 1px;");
            result.Append(tag);

            liTag = new TagBuilder("li") { InnerHtml = result.ToString() };
            ulTag.InnerHtml += string.Format("{0}{1}", liTag, Environment.NewLine);

            return MvcHtmlString.Create(ulTag.ToString());
        }
    }
}