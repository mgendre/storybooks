using System;

namespace StoryBooks.MediaLib
{
    public class MediaLibException : Exception
    {
        public MediaLibException(string? message = null, Exception? innerException = null) :
            base(message, innerException)
        {
        }
    }
}