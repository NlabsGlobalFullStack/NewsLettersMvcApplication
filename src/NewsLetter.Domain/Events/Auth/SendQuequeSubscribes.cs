using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsLetter.Domain.Repositories;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace NewsLetter.Domain.Events.Auth;

public sealed class SendQuequeSubscribes(ISubscribeRepository subscribeRepository) : INotificationHandler<BlogEvent>
{
    public async Task Handle(BlogEvent notification, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory{ HostName ="localhost"};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "newsletters",
            exclusive: false,
            autoDelete: false,
            arguments: null
            );


        var emails = await subscribeRepository.Where(p => p.EmailConfirmed).Select(s => s.Email).ToListAsync(cancellationToken);
        foreach (var email in emails)
        {
            var data = new
            {
                Email = email,
                BlogId = notification.BlogId
            };
            var message = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty, routingKey: "newsletters", basicProperties: null, body: body);

            Console.WriteLine($" [x] {email} sended queue");
        }
        await Task.CompletedTask;
    }
}
