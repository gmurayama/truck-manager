using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace TruckManager.Application.Persistence
{
    public interface IMongoDBService
    {
        IMongoDatabase Instance { get; }

        IMongoCollection<T> GetCollection<T>();
    }
}
