using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class UserEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<User> builder)
        {
            builder.HasData(UserFakeDataSeed.FakeDataUserSeed());
        }
    }
}