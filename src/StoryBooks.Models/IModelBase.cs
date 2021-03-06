using System;

namespace StoryBooks.Models
{
    public interface IModelBase
    {
        public string Id { get; }
        
        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}
