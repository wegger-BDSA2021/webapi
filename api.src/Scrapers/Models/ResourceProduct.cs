using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceBuilder
{

    class ResourceProduct // this will be the createDto
    {
        public string TitleFromUser { get; set; }
        public int UserId { get; set; }
        public string TitleFromSource { get; set; }
        public string Description { get; set; }
        public DateTime TimeOfReference { get; set; }
        public DateTime TimeOfPublication { get; set; }
        public string Url { get; set; }
        public string HostBaseUrl { get; set; }
        public bool IsOfficialDocumentation { get; set; }
        public int InitialRating { get; set; }
        public bool deprecated { get; set; }
        public DateTime LastCheckedForDeprecation { get; set; }
        public bool IsVideo { get; set; }
        public ICollection<string> TagsFoundInSource { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("\nTitle from user: " + TitleFromUser);
            sb.AppendLine();
            sb.Append("\nUser id: " + UserId);
            sb.AppendLine();
            sb.Append("\nTitle from source: " + TitleFromSource);
            sb.AppendLine();
            sb.Append("\nUrl: \n - " + Url);
            sb.AppendLine();
            sb.Append("\nHost base url: " + HostBaseUrl);
            sb.AppendLine();
            sb.Append("\nOfficial documentation: " + IsOfficialDocumentation);
            sb.AppendLine();
            sb.Append("\nVideo: " + IsVideo);
            sb.AppendLine();
            sb.Append("\nTags found:");
            sb.AppendLine();
            TagsFoundInSource.ToList().ForEach(t => sb.Append(" - " + t + "\n"));
            

            return sb.ToString();
        }
    }
}