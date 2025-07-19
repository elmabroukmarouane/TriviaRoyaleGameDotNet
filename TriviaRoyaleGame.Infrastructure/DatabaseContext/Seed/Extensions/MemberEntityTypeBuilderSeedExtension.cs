using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.FakeData;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions
{
    public static class MemberEntityTypeBuilderSeedExtension
    {
        public static void Seed(this EntityTypeBuilder<Member> builder)
        {
            builder.HasData(MemberFakeDataSeed.FakeDataMemberSeed());
        }
    }
}
