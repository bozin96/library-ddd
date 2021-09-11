using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Config
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Core.LibraryAggregate.Library>
    {
        public void Configure(EntityTypeBuilder<Core.LibraryAggregate.Library> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Name).IsRequired(true);
        }
    }
}
