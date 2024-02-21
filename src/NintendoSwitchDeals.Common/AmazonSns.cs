namespace NintendoSwitchDeals.Common;

using System;
using System.Threading.Tasks;

using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

public class AmazonSns
{
    private readonly AmazonSimpleNotificationServiceClient _snsClient = new(RegionEndpoint.USEast1);

    public async Task PublishToTopicAsync(
        string topicArn,
        string messageText,
        string? subject
    )
    {
        PublishRequest request = new() { TopicArn = topicArn, Message = messageText, Subject = subject };

        PublishResponse? response = await _snsClient.PublishAsync(request);

        Console.WriteLine($"Successfully published message ID: {response.MessageId}");
    }
}