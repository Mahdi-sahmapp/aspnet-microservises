
using Catolog.Api.Entities;
using MongoDB.Driver;

namespace Catolog.Api.Data
{
    public interface ICatologContext
    {
        public IMongoCollection<Product> Products { get; }
    }
}
