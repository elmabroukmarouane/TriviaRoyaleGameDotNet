using TriviaRoyaleGame.Business.Redis.Interface;
using StackExchange.Redis;

namespace TriviaRoyaleGame.Business.Redis.Class
{
    public class RedisConnectionFactory(string connectionString) : IRedisConnectionFactory
    {
        #region ATTRIBUTES
        protected readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        #endregion

        #region METHODS
        public ConnectionMultiplexer GetConnectionMultiplexer() => _connectionMultiplexer.Value;
        #endregion
    }
}
