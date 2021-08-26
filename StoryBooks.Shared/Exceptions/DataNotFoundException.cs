using System;

namespace StoryBooks.Shared.Exceptions
{
    public class DataNotFoundException: Exception
    {
        public DataNotFoundException(string? message = null, Exception? e = null) : base(message, e)
        {
        }
    }
}
