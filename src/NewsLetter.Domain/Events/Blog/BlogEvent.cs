using MediatR;

namespace NewsLetter.Domain.Events.Blog;
public sealed record BlogEvent(Guid BlogId) : INotification;
