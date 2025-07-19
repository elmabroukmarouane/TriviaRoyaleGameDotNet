using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Seed();
        }
    }
}
