﻿using TriviaRoyaleGame.Infrastructure.DatabaseContext.Seed.Extensions;
using TriviaRoyaleGame.Infrastructure.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriviaRoyaleGame.Infrastructure.DatabaseContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Role)
                   .HasConversion<int>();
            builder.HasOne(x => x.Member)
                   .WithMany(x => x.Users)
                   .HasForeignKey(x => x.MemberId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.Email)
                   .IsUnique();
            builder.Seed();
        }
    }
}