using NintendoSwitchDeals.Common.Domain;

namespace NintendoSwitchDeals.Common.Services.DealEmailService;

public class DealEmailService(AmazonSns amazonSns) : IDealEmailService
{
    private readonly string _topicArn = Environment.GetEnvironmentVariable("SNS_TOPIC_ARN") ??
                                        throw new ArgumentNullException(nameof(_topicArn),
                                            "SNS_TOPIC_ARN environment variable is not set.");

    public async Task PublishDeal(Deal deal)
    {
        string discountPercentageText = $"{Math.Round(deal.DiscountPercentage)}%";

        string subjectText =
            $"{discountPercentageText} on {deal.Game.Name}!";

        string messageText =
            $"You can now get {discountPercentageText} on {deal.Game.Name}.\n" +
            $"Original Price: {deal.RegularPrice.Amount} {deal.RegularPrice.Currency.Symbol}\n" +
            $"Discounted Price: {deal.DiscountPrice.Amount} {deal.DiscountPrice.Currency.Symbol}\n" +
            $"Link to the game: {deal.Game.Link}";

        await amazonSns.PublishToTopicAsync(_topicArn, messageText, subjectText);
    }
}