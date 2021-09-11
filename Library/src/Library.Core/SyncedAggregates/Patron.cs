using Ardalis.GuardClauses;
using Library.SharedKernel;
using Library.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Library.Core.SyncedAggregate
{
    public class Patron : BaseEntity<Guid>, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public PatronType Type { get; private set; }
        public int MaxNumberOfLendedBooksAtSameTime
        {
            get
            {
                return Type == PatronType.Regular ? 3 : 5;
            }
        }
        public int MaxNumberOfReservedBooksAtSameTime
        {
            get
            {
                return Type == PatronType.Regular ? 0 : 1;
            }
        }
        public Patron(Guid id)
        {
            Id = Guard.Against.Default(id, nameof(id));
        }

        public Patron(Guid id, string firstName, string middleName, string lastName, string email, PatronType type)
        {
            Id = Guard.Against.Default(id, nameof(id));
            FirstName = Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            MiddleName = Guard.Against.NullOrEmpty(middleName, nameof(middleName));
            LastName = Guard.Against.NullOrEmpty(lastName, nameof(lastName));
            Email = Guard.Against.NullOrEmpty(email, nameof(email)); // add real mail validation
            Type = type;
        }
    }
}
