using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Configurations
{
    public class ClientAppLogConfiguration : IEntityTypeConfiguration<ClientAppLog>
    {
        public void Configure(EntityTypeBuilder<ClientAppLog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Seed();
        }
    }
}
