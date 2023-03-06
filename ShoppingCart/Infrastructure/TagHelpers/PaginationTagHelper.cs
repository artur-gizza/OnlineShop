using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text;

namespace ShoppingCart.Infrastructure.TagHelpers
{
    public class PaginationTagHelper : TagHelper
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public string Route { get; set; } = "";
        public string FirstPage { get; set; } = "&laquo;";
        public string LastPage { get; set; } = "&raquo;";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "pagination";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("aria-label", "Page navigation");
            output.Content.SetHtmlContent(CreatePagination());
        }

        private string CreatePagination()
        {
            var content = new StringBuilder();
            content.Append("<ul class='pagination'>");

            if (CurrentPage > TotalPages) CurrentPage = TotalPages;

            if (CurrentPage > 3)
            {
                content.Append($"<li class='page-item'><a class='page-link' href='{Route}'>{FirstPage}</a></li>");
            }

            for(int page = Math.Max(1, CurrentPage - 2); page <= Math.Min(TotalPages, CurrentPage + 2); page++)
            {
                    content.Append($"<li class='page-item {(page == CurrentPage ? "active" : "")}'>" +
                        $"<a class='page-link'href='{Route}?p={page}'>{page}</a></li>");
            }

            if (CurrentPage < TotalPages - 2)
            {
                content.Append($"<li class='page-item'><a class='page-link' href='{Route}?p={TotalPages}'>{LastPage}</a></li>");
            }

            content.Append(" </ul");
            return content.ToString();
        }
    }
}