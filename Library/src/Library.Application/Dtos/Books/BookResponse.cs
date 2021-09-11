using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.Books
{
    public class BookResponse
    {
        public Guid Id { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int NumberOfPages { get; set; }

        public string Genre { get; set; }

        public string Type { get; set; }

        public int NumberOfAvailableCopies { get; set; }

        public BookResponse()
        {

        }

        public BookResponse(Guid id, string isbn, string title, string description, int numberOfPages, string genre, string type, int numberOfAvailableCopies)
        {
            Id = id;
            ISBN = isbn;
            Title = title;
            Description = description;
            NumberOfPages = numberOfPages;
            Genre = genre;
            Type = type;
            NumberOfAvailableCopies = numberOfAvailableCopies;
        }
    }
}