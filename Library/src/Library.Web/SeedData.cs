using Library.Core.LibraryAggregate;
using Library.Core.SyncedAggregate;
using Library.Core.ValueObjects;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Library.Web
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null, null))
            {
                Guid libraryId = Guid.Parse("8DCFD67A-1354-42F2-A0BC-C5731310A249");

                if (!dbContext.Libraries.Any())
                {
                    //libraryId = Guid.NewGuid();
                    Core.LibraryAggregate.Library library =
                        new Core.LibraryAggregate.Library(libraryId, "Alexandria library");

                    dbContext.Libraries.Add(library);
                    dbContext.SaveChanges();
                }

                Guid authorId = Guid.Empty;
                Guid author2Id = Guid.Empty;
                if (!dbContext.Authors.Any())
                {
                    authorId = Guid.NewGuid();
                    Author author =
                        new Author(authorId, "Lev", "Nikolayevich", "Tolstoy", new DateTime(1828, 9, 9), new DateTime(1910, 11, 20));

                    author2Id = Guid.NewGuid();
                    Author author2 =
                        new Author(author2Id, "Ljubodrag", "Duci", "Simonovic", new DateTime(1949, 1, 1), null);

                    dbContext.Authors.Add(author);
                    dbContext.Authors.Add(author2);
                    dbContext.SaveChanges();
                }
                

                if (!dbContext.Books.Any())
                {
                    if (libraryId == Guid.Empty)
                        libraryId = dbContext.Libraries.FirstOrDefault().Id;


                    if (authorId == Guid.Empty)
                        authorId = dbContext.Authors.FirstOrDefault().Id;

                    Book book1 =
                        new Book(Guid.NewGuid(), libraryId, new ISBN("978-3-16-148410-0"), "War and Peace", "War and Peace book", 1225,
                            authorId, Genre.History, BookType.Regular, 4);

                    Book book2 =
                        new Book(Guid.NewGuid(), libraryId, new ISBN("988-3-16-148410-0"), "Resurrection", "Resurrection book", 483,
                            authorId, Genre.Novel, BookType.Restricted, 4);

                    Book book3 =
                    new Book(Guid.NewGuid(), libraryId, new ISBN("998-3-16-148410-0"), "The Death of Ivan Ilyich", "Example book", 103,
                        authorId, Genre.Novella, BookType.Regular, 3);

                    Book book4 =
                        new Book(Guid.NewGuid(), libraryId, new ISBN("998-3-16-148410-0"), "The Kreutzer Sonata", "The Kreutzer Sonata book", 118,
                            authorId, Genre.Novella, BookType.Regular, 2);

                    Book book5 =
                        new Book(Guid.NewGuid(), libraryId, new ISBN("998-4-16-148410-0"), "Sport, kapitalizam, destrukcija", "Duci Duci", 234,
                            author2Id, Genre.Sociology, BookType.Regular, 10);
                    Book book6 =
                        new Book(Guid.NewGuid(), libraryId, new ISBN("998-4-16-148410-0"), "Filozofski aspekti modernog olimpizma", "Duci Duci", 434,
                            author2Id, Genre.Philosophy, BookType.Regular, 10);

                    dbContext.Books.Add(book1);
                    dbContext.Books.Add(book2);
                    dbContext.Books.Add(book3);
                    dbContext.Books.Add(book4);
                    dbContext.Books.Add(book5);
                    dbContext.Books.Add(book6);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Patrons.Any())
                {
                    Patron patron1 =
                        new Patron(Guid.NewGuid(), "Jovan", "Petar", "Jocic", "test@example.com", PatronType.Regular);
                    Patron patron2 =
                        new Patron(Guid.NewGuid(), "Marko", "Dusko", "Markovic", "test2@example.com", PatronType.Premium);

                    dbContext.Patrons.Add(patron1);
                    dbContext.Patrons.Add(patron2);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
