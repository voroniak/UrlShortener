using MongoDB.Driver;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Interfaces.Context
{
    public interface IDbContext
    {
        IMongoCollection<UrlManagement> UrlManagements { get; }
    }
}
