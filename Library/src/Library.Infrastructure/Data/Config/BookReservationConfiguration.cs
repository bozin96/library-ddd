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
    class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
    {
        public void Configure(EntityTypeBuilder<BookReservation> builder)
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
