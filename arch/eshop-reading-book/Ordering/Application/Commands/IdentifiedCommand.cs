using MediatR;
using System;

namespace Ordering.Application.Commands
{
    public class IdentifiedCommand<T,R> : IRequest<R>
    {
        public T Command { get; }
        public Guid Id { get; }

        public IdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
