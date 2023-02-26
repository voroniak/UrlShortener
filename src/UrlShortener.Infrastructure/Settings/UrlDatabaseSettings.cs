﻿namespace UrlShortener.Infrastructure.Settings
{
    public class UrlDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UrlsCollectionName { get; set; } = null!;

    }
}
