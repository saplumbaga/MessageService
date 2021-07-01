using MessageService.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Configuration
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IpAddr).HasMaxLength(32);

            builder.HasOne(x => x.Message)
                .WithMany(x => x.Replies)
                .HasForeignKey(x => x.MessageId);
        }
    }
}
