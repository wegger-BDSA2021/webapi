using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using HtmlAgilityPack;

namespace ResourceBuilder
{
    abstract class Template
    {
        protected string url;
        protected static ICollection<string> QueryTerms { get; set; }

        #pragma warning disable 1998
        public async Task Parse(string content, ResourceCreateDTOServer product, ICollection<string> queryTerms)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            url = product.Url;
            QueryTerms = queryTerms;

            product.TitleFromSource = this.GetTitle(doc);
            product.TagsFoundInSource = this.GetTags(doc).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        #pragma warning restore 1998

        protected string GetTitle(HtmlDocument _doc)
        {
            var title = _doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        protected abstract Task<ICollection<string>> GetTags(HtmlDocument _doc);
    }
}