using MediatR;

namespace NewsLetter.Application.Events.Blog;
public sealed record BlogEvent(Guid BlogId) : INotification;
