﻿// <auto-generated />
using System;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210910162457_Initial-Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.Core.LibraryAggregate.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentAvailableNumberOfCopies")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<Guid>("LibraryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfCopies")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("LibraryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.BookLending", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LibraryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryId");

                    b.ToTable("BookLendings");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.BookReservation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LibraryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PatronId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LibraryId");

                    b.ToTable("BookReservations");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.Library", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("Library.Core.SyncedAggregate.Patron", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patrons");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.Book", b =>
                {
                    b.HasOne("Library.Core.LibraryAggregate.Author", null)
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Core.LibraryAggregate.Library", null)
                        .WithMany("Books")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Library.Core.ValueObjects.ISBN", "ISBN", b1 =>
                        {
                            b1.Property<Guid>("BookId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ISBN");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("ISBN");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.BookLending", b =>
                {
                    b.HasOne("Library.Core.LibraryAggregate.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Core.LibraryAggregate.Library", null)
                        .WithMany("BookLendings")
                        .HasForeignKey("LibraryId");

                    b.OwnsOne("Library.SharedKernel.DateTimeRange", "DateTimeRange", b1 =>
                        {
                            b1.Property<Guid>("BookLendingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2")
                                .HasColumnName("DateTimeRange_End");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2")
                                .HasColumnName("DateTimeRange_Start");

                            b1.HasKey("BookLendingId");

                            b1.ToTable("BookLendings");

                            b1.WithOwner()
                                .HasForeignKey("BookLendingId");
                        });

                    b.Navigation("Book");

                    b.Navigation("DateTimeRange");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.BookReservation", b =>
                {
                    b.HasOne("Library.Core.LibraryAggregate.Library", null)
                        .WithMany("BookReservations")
                        .HasForeignKey("LibraryId");

                    b.OwnsOne("Library.SharedKernel.DateTimeRange", "DateTimeRange", b1 =>
                        {
                            b1.Property<Guid>("BookReservationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2")
                                .HasColumnName("DateTimeRange_End");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2")
                                .HasColumnName("DateTimeRange_Start");

                            b1.HasKey("BookReservationId");

                            b1.ToTable("BookReservations");

                            b1.WithOwner()
                                .HasForeignKey("BookReservationId");
                        });

                    b.Navigation("DateTimeRange");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Library.Core.LibraryAggregate.Library", b =>
                {
                    b.Navigation("BookLendings");

                    b.Navigation("BookReservations");

                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
