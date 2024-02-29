using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

using NintendoSwitchDeals.Scraper.Domain;

namespace NintendoSwitchDeals.Scraper.Services.NotificationService;

public static class NotificationService
{
    private static readonly AmazonSimpleNotificationServiceClient Client = new();

    private static string s_topicArn = Environment.GetEnvironmentVariable("SNS_TOPIC_ARN") ??
                                       throw new ArgumentNullException(nameof(s_topicArn),
                                           "SNS_TOPIC_ARN environment variable is not set.");

    public static async Task PublishGameDiscount(GameDiscount gameDiscount)
    {
        string discountPercentageText = $"{Math.Round(gameDiscount.DiscountPercentage)}%";

        string subjectText =
            $"{discountPercentageText} on {gameDiscount.Game.Name}!";

        string messageText =
            $"You can now get {discountPercentageText} on {gameDiscount.Game.Name}.\n" +
            $"Original Price: {gameDiscount.RegularPrice.Amount} €\n" +
            $"Discounted Price: {gameDiscount.DiscountPrice.Amount} €\n" +
            $"Link to the game: {gameDiscount.Game.Url}";

        PublishRequest request = new() { TopicArn = s_topicArn, Message = messageText, Subject = subjectText };

        PublishResponse? response = await Client.PublishAsync(request);

        Console.WriteLine($"Successfully published message ID: {response.MessageId}");
        Console.WriteLine($"Message sent to topic: {messageText}");
    }
}