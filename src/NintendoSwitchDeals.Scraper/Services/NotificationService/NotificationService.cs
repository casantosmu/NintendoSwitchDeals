using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using NintendoSwitchDeals.Scraper.Data;
using NintendoSwitchDeals.Scraper.Domain;
using NintendoSwitchDeals.Scraper.Models;

namespace NintendoSwitchDeals.Scraper.Services.NotificationService;

public class NotificationService(ILogger<NotificationService> logger, ScraperContext scraperContext)
    : INotificationService
{
    private readonly AmazonSimpleNotificationServiceClient _client = new();

    private readonly string _topicArn = Environment.GetEnvironmentVariable("SNS_TOPIC_ARN") ??
                                        throw new Exception(
                                            "SNS_TOPIC_ARN environment variable is not set.");

    public async Task PublishGameDiscount(GameDiscount gameDiscount)
    {
        bool gameExists = await scraperContext.Notifications.AnyAsync(n => n.GameId == gameDiscount.Game.GameId);

        if (gameExists)
        {
            throw new Exception(
                $"Notification for ({gameDiscount.Game.GameId}) {gameDiscount.Game.Name} already exists.\n" +
                $"Discount amount: {gameDiscount.DiscountPrice.Amount}\n" +
                $"Discount end date: {gameDiscount.DiscountPrice.EndDateTime})");
        }

        string discountPercentageText = $"{Math.Round(gameDiscount.DiscountPercentage)}%";

        string subjectText =
            $"{discountPercentageText} on {gameDiscount.Game.Name}!";

        string messageText =
            $"You can now get {discountPercentageText} on {gameDiscount.Game.Name}.\n" +
            $"Original Price: {gameDiscount.RegularPrice.Amount} €\n" +
            $"Discounted Price: {gameDiscount.DiscountPrice.Amount} €\n" +
            $"Offer starts at: {gameDiscount.DiscountPrice.StartDateTime.Date.ToShortDateString()}\n" +
            $"Offer ends at: {gameDiscount.DiscountPrice.EndDateTime.Date.ToShortDateString()}\n" +
            $"Link to the game: {gameDiscount.Game.Url}";

        PublishRequest request = new() { TopicArn = _topicArn, Message = messageText, Subject = subjectText };

        await using IDbContextTransaction transaction = await scraperContext.Database.BeginTransactionAsync();

        Notification notification = new()
        {
            DiscountPrice = gameDiscount.DiscountPrice.Amount,
            GameId = gameDiscount.Game.GameId,
            EndDateTime = gameDiscount.DiscountPrice.EndDateTime
        };

        scraperContext.Notifications.Add(notification);
        await scraperContext.SaveChangesAsync();

        PublishResponse response = await _client.PublishAsync(request);

        await transaction.CommitAsync();

        logger.LogInformation("Successfully published message ID: {MessageId}", response.MessageId);
        logger.LogInformation("Message sent to topic: {messageText}", messageText);
    }
}