using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Web;
using System.Text;
using System.Collections.Concurrent;

namespace ResourceBuilder
{
    class ScrapeVideo : Template
    {
        protected override async Task<ICollection<string>> GetTags(HtmlDocument _doc)
        {
            var _details = await GetVideoDetails();

            var sb = new StringBuilder();
            sb.Append(_details.Description.ToLower() + " ");
            sb.Append(_details.Title.ToLower() + " ");
            sb.Append(_details.ChannelTitle.ToLower() + " ");

            if (_details.Tags.Any())
                _details.Tags.ToList().ForEach(t => sb.Append(t.ToLower() + " "));

            var cleanedText = sb.ToString();
            var foundTagsConcurrent = new ConcurrentBag<string>();

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

        private async Task<VideoDetails> GetVideoDetails()
        {
            using (var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBiPzfkPlca3Tm3iryIBjTOPj6EdNuFK5s",
            }))
            {
                var searchRequest = youtubeService.Videos.List("snippet");
                var uri = new Uri(url);
                var query = HttpUtility.ParseQueryString(uri.Query);
                var id = query["v"];
                searchRequest.Id = id;


                var searchResponse = await searchRequest.ExecuteAsync();
                var youTubeVideo = searchResponse.Items.FirstOrDefault();
                if (youTubeVideo != null)
                {
                    var details = new VideoDetails
                    {
                        VideoId = youTubeVideo.Id,
                        Description = youTubeVideo.Snippet.Description ?? " ",
                        Title = youTubeVideo.Snippet.Title ?? " ",
                        ChannelTitle = youTubeVideo.Snippet.ChannelTitle ?? " ",
                        PublicationDate = youTubeVideo.Snippet.PublishedAt,
                        Tags = youTubeVideo.Snippet.Tags ?? new List<string>()
                    };

                    return details;
                }
                else 
                {
                    throw new Exception();
                }
            }
        }

        private class VideoDetails
        {
            public string VideoId { get; set; }
            public string Description { get; set; }
            public string Title { get; set; }
            public string ChannelTitle { get; set; }
            public DateTime? PublicationDate { get; set; }
            public IList<string> Tags { get; set; }
        }
    }
}