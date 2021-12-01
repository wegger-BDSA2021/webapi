using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Web;

namespace ResourceBuilder
{
    class ScrapeVideo : Template
    {
        protected override async Task<ICollection<string>> GetTags(HtmlDocument _doc)
        {
            var _details = await GetVideoDetails();
            return _details.Tags;
        }

        private async Task<VideoDetails> GetVideoDetails()
        {

            // api key: AIzaSyBiPzfkPlca3Tm3iryIBjTOPj6EdNuFK5s
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
                        Description = youTubeVideo.Snippet.Description,
                        Title = youTubeVideo.Snippet.Title,
                        ChannelTitle = youTubeVideo.Snippet.ChannelTitle,
                        PublicationDate = youTubeVideo.Snippet.PublishedAt,
                        Tags = youTubeVideo.Snippet.Tags
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