using System;

namespace Services
{
    public sealed class TagsCollection
    {
        private TagsCollection()    
    {    
        
    }    
    private static readonly Lazy<TagsCollection> lazy = new Lazy<TagsCollection>(() => new TagsCollection());    
    public static TagsCollection Instance    
    {    
        get    
        {    
            return lazy.Value;    
        }    
    } 
    }
}