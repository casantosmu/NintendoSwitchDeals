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
            throw new GameNotFoundException(gameDiscount.Game);
        }

        bool shouldNotify = await ShouldNotifyGameDiscount(gameDiscount);
        if (!shouldNotify)
        {
            throw new NotificationConflictException(gameDiscount);
        }

        GameDiscountMessage gameDiscountMessage = new(gameDiscount);

        PublishRequest request = new()
        {
            TopicArn = awsOptions.Value.SnsTopicArn,
            Message = gameDiscountMessage.Message,
            Subject = gameDiscountMessage.Subject
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
            "Successfully published message with ID: '{MessageId}' for game discount: {GameDiscount}",
            response.MessageId, gameDiscount);
        logger.LogDebug("Discount notification message details:\n{GameDiscountMessage}", gameDiscountMessage);
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