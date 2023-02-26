namespace UrlShortener.Application.Common.Exceptions
{
    public class UrlCreationException : Exception
    {
        public UrlCreationException(string message)
        : base(message)
        {

        }
    }
}
