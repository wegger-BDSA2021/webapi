using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Resource.Builder
{
    class ScrapeArticle : Template
    {
        protected override async Task<ICollection<string>> GetTags(HtmlDocument _doc)
        {
            var foundTagsConcurrent = new ConcurrentBag<string>();
            var foundTags = new List<string>();

            var paragraphs = _doc.DocumentNode.SelectNodes("//p");
            var bullets = _doc.DocumentNode.SelectNodes("//li");

            StringBuilder sb = new StringBuilder();

            if (paragraphs is not null)
            {
                foreach (var item in paragraphs)
                {
                    sb.Append(item.InnerText.ToLower() + " ");
                }
            }

            if (bullets is not null)
            {
                foreach (var item in bullets)
                {
                    sb.Append(item.InnerText.ToLower() + " ");
                }
            }

            var cleanedText = sb.ToString();

            Parallel.ForEach
            (
                QueryTerms, tag =>
                {
                    if (cleanedText.Contains(tag.ToLower()))
                    {
                        foundTagsConcurrent.Add(tag);
                    }
                }
            );

            var result = foundTagsConcurrent.ToList();
            return await Task.FromResult(result);
        }
    }
}