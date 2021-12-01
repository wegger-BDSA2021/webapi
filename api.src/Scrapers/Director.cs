using System;
using System.Threading.Tasks;
using Data;

namespace ResourceBuilder
{
    class Director
    {
        private Builder _builder;
        public Director(Builder builder)
        {
            this._builder = builder;
        }
        
        public async Task<ResourceCreateDTOServer> Make()
        {
            // _builder.CheckIfUrlIsValid();
            await _builder.RetrieveHtml();
            _builder.SetUrl();
            _builder.SetHostBaseUrl();
            _builder.SetUserTitle();
            _builder.SetDescription();
            _builder.SetTimeOfReference();
            _builder.SetUserId();
            _builder.SetInitialRating();
            _builder.SetOfficialDocumentation();
            _builder.CheckForVideo();
            await _builder.ScrapeTagsAndDate();

            return _builder.GetResult();
        }

        // public async Task<ResourceProduct> UpdateResource()
        // {
        //     throw new NotImplementedException();
        // }

    }


}