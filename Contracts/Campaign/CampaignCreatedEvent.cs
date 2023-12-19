namespace Contracts.Campaign;

public class CampaignCreatedEvent
{
    public Guid CampaignId { get; set; }
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }
    public int PrizeCount { get; set; }
}