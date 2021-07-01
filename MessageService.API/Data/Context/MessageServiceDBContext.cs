using MessageService.API.Data.Configuration;
using MessageService.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.API.Data
{
    public class MessageServiceDBContext : DbContext
    {
     
        public MessageServiceDBContext(DbContextOptions<MessageServiceDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new LogConfiguration());
            builder.ApplyConfiguration(new ReplyConfiguration());
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
