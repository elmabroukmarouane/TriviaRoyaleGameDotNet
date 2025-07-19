using StackExchange.Redis;

namespace TriviaRoyaleGame.Business.Redis.Interface
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer GetConnectionMultiplexer();
    }
}
