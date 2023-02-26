namespace UrlShortener.Infrastructure.Settings
{
    public class UrlDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UrlsCollectionName { get; set; }
    }
}