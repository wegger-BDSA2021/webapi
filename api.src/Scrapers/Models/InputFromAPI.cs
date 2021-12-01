using System.Collections.Generic;

namespace Resource.Builder
{
    class InputFromAPI
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public ICollection<string> TagsCategories { get; set; }
        public int InitialRating { get; set; }
    }

}