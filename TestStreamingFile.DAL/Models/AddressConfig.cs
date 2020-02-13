using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestStreamingFile.DAL.Models
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        void IEntityTypeConfiguration<Address>.Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.UNCPath).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(false);
        }
    }
}
