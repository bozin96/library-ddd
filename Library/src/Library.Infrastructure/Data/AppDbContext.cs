using Ardalis.EFCore.Extensions;
using Library.Core.LibraryAggregate;
using Library.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Core.SyncedAggregate;
using Library.SharedKernel.Interfaces;
using System;

namespace Library.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IMediatorHandler _mediatorHandler;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediator = mediator;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Core.LibraryAggregate.Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLending> BookLendings { get; set; }
        public DbSet<BookReservation> BookReservations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // Ignore events if no dispatcher provided.
            if (_mediator == null) return result;

            // Dispatch events only if save was successful.
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<Guid>>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    //await _mediator.Publish(domainEvent).ConfigureAwait(false);
                    await _mediatorHandler.RaiseEvent(domainEvent);

                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}