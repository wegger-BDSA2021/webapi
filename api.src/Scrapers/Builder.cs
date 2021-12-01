using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Resource.Builder
{

    class Builder
    {
        public ResourceProduct _product;
        public InputFromAPI _input;
        private string _content; 

        public Builder(InputFromAPI input)
        {
            this.Reset();
            this._input = input;
        }

        public ResourceProduct GetResult() => this._product;

        public void Reset()
        {
            this._product = new ResourceProduct();
        }

        public void CheckIfUrlIsValid()
        {
            Uri uriResult; 
            bool isValid = Uri.TryCreate(_input.Url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValid)
            {
                throw new Exception();
            }
        }

        public async Task RetrieveHtml()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_input.Url);

            var httpStatusCode = response.StatusCode;
            if (httpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception();
            }

            var content = await response.Content.ReadAsStringAsync();
            _content = content;
            client.Dispose();
        }


        public void SetHostBaseUrl()
        {
            Uri uri = new Uri(_input.Url);
            _product.HostBaseUrl = uri.Host;
        }

        public void SetUrl() => _product.Url = _input.Url;

        public void SetUserTitle() => _product.TitleFromUser = _input.Title;

        public void SetDescription() => _product.Description = _input.Description;

        public void SetTimeOfReference() => _product.TimeOfReference = DateTime.Now;

        public void SetInitialRating() => _product.InitialRating = _input.InitialRating;

        public void SetUserId() => _product.UserId = _input.UserId;

        public void SetOfficialDocumentation()
        {
            var officials = new List<string>();
            officials.Add("docs.microsoft.com");
            officials.Add("www.uml-diagrams.org");

            if (officials.Contains(_product.HostBaseUrl))
            {    
                _product.IsOfficialDocumentation = true;
            }
            else 
            {
                _product.IsOfficialDocumentation = false;
            }
        }

        public void CheckForVideo()
        {
            if (_product.HostBaseUrl == "www.youtube.com")
            {
                _product.IsVideo = true;
            }
            else
            {
                _product.IsVideo = false;
            }
        }

        private bool IsVideo() => _product.IsVideo;
        private async Task TemplateClient(Template template, string content, ResourceProduct product) => await template.Parse(content, product);

        public async Task ScrapeTagsAndDate()
        {
            if (IsVideo())
            {
                await TemplateClient(new ScrapeVideo(), _content, _product);
            }
            else 
            {
                await TemplateClient(new ScrapeArticle(), _content, _product);
            }
        }


    }

}