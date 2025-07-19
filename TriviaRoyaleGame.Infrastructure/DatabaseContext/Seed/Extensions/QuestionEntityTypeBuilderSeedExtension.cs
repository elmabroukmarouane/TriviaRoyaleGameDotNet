using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class QuestionEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Question> builder)
        {
            builder.HasData(QuestionFakeDataSeed.FakeDataQuestionSeed());
        }
    }
}