using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class ClientAppLogEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<ClientAppLog> builder)
        {
            builder.HasData(ClientAppLogFakeDataSeed.FakeDataClientAppLogSeed());
        }
    }
}
