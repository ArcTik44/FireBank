using LiteDB;
using System;

namespace FireBank.Services
{
    public class DatabaseService : IDisposable
    {
        private readonly LiteDatabase _db;

        public DatabaseService()
        {
            _db = new LiteDatabase("FireBank.db");
        }

        public ILiteCollection<T> GetCollection<T>(string name)
            => _db.GetCollection<T>(name);

        public void Dispose() => _db.Dispose();
    }
}