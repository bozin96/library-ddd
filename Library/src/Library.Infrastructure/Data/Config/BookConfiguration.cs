using Library.Core.LibraryAggregate;
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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.OwnsOne(p => p.ISBN, p =>
            {
                p.Property(pp => pp.Value).HasColumnName("ISBN");
            });
            builder.Property(p => p.Title)
                .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH)
                .IsRequired(true);
            builder.Property(p => p.Description)
                .HasMaxLength(ColumnConstants.DEFAULT_DESCRPTION_LENGTH);
            builder.Property(p => p.NumberOfPages)
                .IsRequired(true);
            builder.Property(p => p.NumberOfCopies)
                .IsRequired(true);
            builder.Ignore(p => p.MinimumNumberOfCopies);
        }
    }
}
