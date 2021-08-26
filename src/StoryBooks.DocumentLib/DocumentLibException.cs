using System;

namespace StoryBooks.DocumentLib
{
    public class DocumentLibException : Exception
    {
        public DocumentLibException(string? message = null, Exception? innerException = null) :
            base(message, innerException)
        {
        }
    }
}