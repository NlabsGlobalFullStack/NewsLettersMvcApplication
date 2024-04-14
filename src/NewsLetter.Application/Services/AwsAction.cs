using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Domain.Repositories;
using System.Text;

namespace NewsLetter.Application.Services;
public sealed class AwsAction
{
    private readonly IServiceProvider _serviceProvider;

    public AwsAction(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ExecuteAsync(string? title, CancellationToken stoppingToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var subscribeRepository = scope.ServiceProvider.GetRequiredService<ISubscribeRepository>();

        var emails = await subscribeRepository.Where(p => p.EmailConfirmed).Select(s => s.Email).ToListAsync(stoppingToken);

        var accessKey = "12313";
        var secretKey = "123123213";
        var region = Amazon.RegionEndpoint.USEast1;

        var sqsClient = new AmazonSQSClient(accessKey, secretKey, region);

        var queueResponse = await sqsClient.GetQueueUrlAsync("newsletters", stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueResponse.QueueUrl,
            AttributeNames = new List<string> { "All" },
            MessageAttributeNames = new List<string> { "All" }
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

            foreach (var message in response.Messages)
            {
                await Console.Out.WriteLineAsync($"Message Id: {message.MessageId} ");
                var body = Encoding.UTF8.GetBytes(title!);

                throw new ArgumentException("Something went wrong");

                await sqsClient.DeleteMessageAsync(queueResponse.QueueUrl, message.ReceiptHandle);
            }
            await Task.Delay(100, stoppingToken);
        }


        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueResponse.QueueUrl,
            MessageBody = Encoding.UTF8.GetBytes(title!).ToString(),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                    {
                        "MessageType", new MessageAttributeValue
                        {
                            DataType = "String",
                            StringListValues = emails
                        }
                    }
                }
        };

        var sendingResponse = await sqsClient.SendMessageAsync(sendMessageRequest, stoppingToken);
    }
}
