using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Catalog
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            CachedRepository<int> cachedRepository = new CachedRepository<int>();
            await cachedRepository.GetMyEntityAsync(1);
        }
    }

    public class CachedRepository<MyEntity>
    {
        //private static ConnectionMultiplexer Connection;
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() => 
        {
            string connectionString = ConfigurationManager.AppSettings["CacheConnection"].ToString();
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        private const double DefaultExpirationTimeInMinutes = 5;
        public async Task<MyEntity> GetMyEntityAsync(int id)
        {
            var keyStr = $"MyEnity: {id}";
            RedisKey key = new RedisKey(keyStr);
            var cache = Connection.GetDatabase();

            var json = cache.StringGet(keyStr);
            var value = string.IsNullOrEmpty(json) ? default(MyEntity) : JsonConvert.DeserializeObject<MyEntity>(json);
            if (value == null)
            {
                value = default(MyEntity);

                if (value != null)
                {
                    await cache.StringSetAsync(keyStr, JsonConvert.SerializeObject(value)).ConfigureAwait(false);
                    await cache.KeyExpireAsync(keyStr, TimeSpan.FromMinutes(DefaultExpirationTimeInMinutes)).ConfigureAwait(false);
                }
            }

            return value;
        }

        public async Task Update(MyEntity entity)
        {
            var cache = Connection.GetDatabase();
            var id = 1;// entity.Id
            var key = $"MyEntity:{id}";
            await cache.KeyDeleteAsync(key).ConfigureAwait(false);
        }
    }
}
