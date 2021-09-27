using Ardalis.GuardClauses;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.Core.SyncedAggregate
{
    public class Author : BaseEntity<Guid>, IAggregateRoot
    {
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public DateTime DateOfBirth { get; private set; }

        public DateTime? DateOfDeath { get; private set; }

        //public IList<Book> Books { get; private set; } = new List<Book>();

        public Author(Guid id)
        {
            Id = id;
            //Books = new List<Book>();
        }

        public Author(
            Guid id,
            string firstName,
            string middleName,
            string lastName,
            DateTime dateOfBirth,
            DateTime? dateOfDeath)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            MiddleName = Guard.Against.NullOrEmpty(middleName, nameof(middleName));
            LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
            DateOfBirth = Guard.Against.Null(dateOfBirth, nameof(dateOfBirth));
            DateOfDeath = dateOfDeath;
        }
    }
}
