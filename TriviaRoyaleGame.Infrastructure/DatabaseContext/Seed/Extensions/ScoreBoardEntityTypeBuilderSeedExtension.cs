using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class ScoreBoardEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<ScoreBoard> builder)
        {
            builder.HasData(ScoreBoardFakeDataSeed.FakeDataScoreBoardSeed());
        }
    }
}