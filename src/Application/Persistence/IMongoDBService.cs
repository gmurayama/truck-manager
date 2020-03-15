using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TruckManager.Application.Persistence
{
    public interface IMongoDBService
    {
        IMongoDatabase Instance { get; }

        IMongoCollection<T> GetCollection<T>();

        IMongoQueryable<T> GetCollectionAsQueryable<T>();
    }
}
