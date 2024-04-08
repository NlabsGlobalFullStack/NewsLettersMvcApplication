using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using Newsletter.Consumer;
using Newsletter.Consumer.Context;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var services = new ServiceCollection();

services.AddFluentEmail("turkmvc@gmail.com").AddSmtpSender("localhost", 2525);
var serviceProvider = services.BuildServiceProvider();

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
            queue: "newsletters",
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine();

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var response = JsonSerializer.Deserialize<ResponseDto>(message);
    if (response is null)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine("Response is empty or null");
    }

    var context = new AppDbContext();

    var blog = context.Blogs.Find(response!.BlogId);
    if (blog is null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Blog not found");
    }

    var fluentEmail = serviceProvider.GetRequiredService<IFluentEmail>();

    var sendResponse = fluentEmail.To(response.Email).Subject(blog!.Title).Body(blog!.Content, true).Send();

    if (!sendResponse.Successful)
    {
        Console.WriteLine($" [*] try to {response.Email} blog send but got an error");
    }
    else
    {
        Console.WriteLine($" [*] {response.Email} blog sended");
    }
};

channel.BasicConsume(queue: "newsletters", autoAck: true, consumer: consumer);

Console.BackgroundColor = ConsoleColor.White;
Console.WriteLine(" Press [enter] to exit.");

Console.ReadLine();