using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NintendoSwitchDiscounts.Common.Data;
using NintendoSwitchDiscounts.Common.Domain;
using NintendoSwitchDiscounts.Common.Exceptions;
using NintendoSwitchDiscounts.Common.Models;
using NintendoSwitchDiscounts.Common.Options;
using NintendoSwitchDiscounts.Common.Services.GameService;

namespace NintendoSwitchDiscounts.Common.Services.NotificationService;

public class NotificationService(
    ILogger<NotificationService> logger,
    IOptions<NotificationOptions> awsOptions,
    NintendoSwitchDiscountsContext context,
    IGameService gameService)
    : INotificationService
{
    private readonly AmazonSimpleNotificationServiceClient _client = new();

    public async Task NotifyGameDiscount(GameDiscount gameDiscount)
    {
        bool gameExists = await gameService.GameExists(gameDiscount.Game);

        if (!gameExists)
        {
            throw new GameNotFound(gameDiscount.Game);
        }

        bool shouldNotify = await ShouldNotifyGameDiscount(gameDiscount);

        if (!shouldNotify)
        {
            throw new NotificationConflict(gameDiscount);
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

        PublishRequest request = new()
        {
            TopicArn = awsOptions.Value.SnsTopicArn, Message = messageText, Subject = subjectText
        };

        Notification notification = new()
        {
            DiscountPrice = gameDiscount.DiscountPrice.Amount,
            GameId = gameDiscount.Game.GameId,
            EndDateTime = gameDiscount.DiscountPrice.EndDateTime
        };

        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

        context.Notifications.Add(notification);
        await context.SaveChangesAsync();

        PublishResponse response = await _client.PublishAsync(request);

        await transaction.CommitAsync();

        logger.LogInformation(
            "Successfully published message ID: {MessageId}\nSubject: {subjectText}\nMessage: {messageText}",
            response.MessageId, subjectText, messageText);
    }

    public async Task<bool> ShouldNotifyGameDiscount(GameDiscount gameDiscount)
    {
        bool shouldNotifyGameDiscount = !await context.Notifications.AnyAsync(n =>
            n.GameId == gameDiscount.Game.GameId && n.EndDateTime >= DateTime.Now &&
            n.DiscountPrice <= gameDiscount.DiscountPrice.Amount);

        string notificationStatus = shouldNotifyGameDiscount ? "publish" : "not publish";

        logger.LogDebug("Should {NotificationStatus} notification for game discount: {gameDiscount}",
            notificationStatus, gameDiscount);

        return shouldNotifyGameDiscount;
    }
}