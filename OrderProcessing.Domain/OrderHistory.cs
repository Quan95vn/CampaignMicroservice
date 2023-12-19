namespace OrderProcessing.Domain;

public class OrderHistory
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public decimal TotalAmountSpent { get; set; }
}