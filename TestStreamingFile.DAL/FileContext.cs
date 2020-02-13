using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestStreamingFile.DAL.Models;

namespace TestStreamingFile.DAL
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options) { }

        public DbSet<FileDescription> FileDescriptions { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FileDescription>().HasKey(m => m.Id);
            builder.ApplyConfiguration(new AddressConfig());
        }

    }
}
