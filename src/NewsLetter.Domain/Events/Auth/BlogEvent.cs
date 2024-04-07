using MediatR;

namespace NewsLetter.Domain.Events.Auth;
public sealed record BlogEvent(Guid BlogId) : INotification;
