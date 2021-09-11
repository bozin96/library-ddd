using Library.Core.LibraryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Config
{
    class BookLendingConfiguration : IEntityTypeConfiguration<BookLending>
    {
        public void Configure(EntityTypeBuilder<BookLending> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.OwnsOne(p => p.DateTimeRange, p =>
            {
                p.Property(pp => pp.Start)
                .HasColumnName("DateTimeRange_Start");
                p.Property(pp => pp.End)
                .HasColumnName("DateTimeRange_End");
            });
        }
    }
}
