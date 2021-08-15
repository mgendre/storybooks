using System;

namespace StoryBooks.Models
{
    public interface IModelBase
    {
        public string Id { get; }
        
        public DateTime CreationDate { get; }
        
        public DateTime ModificationDate { get; }
    }
}
