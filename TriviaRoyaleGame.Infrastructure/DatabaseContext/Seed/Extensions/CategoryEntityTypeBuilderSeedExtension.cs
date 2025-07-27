using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class CategoryEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CategoryFakeDataSeed.FakeDataCategorySeed());
        }
    }
}
