using Library.Core.LibraryAggregate;
using Library.Core.SyncedAggregate;
using Library.Infrastructure.Data.Config.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data.Config
{
    public class PatronConfiguration : IEntityTypeConfiguration<Patron>
    {
        public void Configure(EntityTypeBuilder<Patron> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.FirstName)
                .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH)
                .IsRequired(true);
            builder.Property(p => p.MiddleName)
                .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(p => p.LastName)
                .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH)
                .IsRequired(true);
            builder.Property(p => p.Email)
                .IsRequired(true);
            builder.Ignore(p => p.MaxNumberOfLendedBooksAtSameTime);
            builder.Ignore(p => p.MaxNumberOfReservedBooksAtSameTime);
        }
    }
}
