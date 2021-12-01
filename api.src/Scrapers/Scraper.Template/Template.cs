using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using HtmlAgilityPack;

namespace ResourceBuilder
{
    abstract class Template
    {
        protected string url;

        // instead of this, retrieve tags from tagRepository
        protected static string[] QueryTerms { get; } =
        {
            "anglesharp", "docker",
            "dotnet" , ".NET",
            "web scraping", "syntax",
            "xpath", "html",
            "github", "htmlagilitypack",
            "c#", "pascal", "DTO",
            "design pattern", "stopwatch",
            "linq", "c", "c++"
        };

        public async Task Parse(string content, ResourceCreateDTOServer product)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            url = product.Url;

            product.TitleFromSource = this.GetTitle(doc);
            product.TagsFoundInSource = this.GetTags(doc).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        protected string GetTitle(HtmlDocument _doc)
        {
            var title = _doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        protected abstract Task<ICollection<string>> GetTags(HtmlDocument _doc);
    }
}