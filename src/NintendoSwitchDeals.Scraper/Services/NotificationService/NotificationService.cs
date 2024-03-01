using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

using Microsoft.Extensions.Logging;

using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NotificationService;

public class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    private readonly AmazonSimpleNotificationServiceClient _client = new();

    private readonly string _topicArn = Environment.GetEnvironmentVariable("SNS_TOPIC_ARN") ??
                                        throw new Exception(
                                            "SNS_TOPIC_ARN environment variable is not set.");

    public async Task PublishGameDiscount(GameDiscount gameDiscount)
    {
        string discountPercentageText = $"{Math.Round(gameDiscount.DiscountPercentage)}%";

        string subjectText =
            $"{discountPercentageText} on {gameDiscount.Game.Name}!";

        string messageText =
            $"You can now get {discountPercentageText} on {gameDiscount.Game.Name}.\n" +
            $"Original Price: {gameDiscount.RegularPrice.Amount} €\n" +
            $"Discounted Price: {gameDiscount.DiscountPrice.Amount} €\n" +
            $"Link to the game: {gameDiscount.Game.Url}";

        PublishRequest request = new() { TopicArn = _topicArn, Message = messageText, Subject = subjectText };

        PublishResponse response = await _client.PublishAsync(request);

        logger.LogInformation("Successfully published message ID: {MessageId}", response.MessageId);
        logger.LogInformation("Message sent to topic: {messageText}", messageText);
    }
}