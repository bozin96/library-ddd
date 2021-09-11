using MediatR;
using System;

namespace Library.SharedKernel
{
    /// <summary>
    /// Base types for all Domain Events which track state using a given Id.
    /// </summary>
    public abstract class BaseDomainEvent : INotification
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}