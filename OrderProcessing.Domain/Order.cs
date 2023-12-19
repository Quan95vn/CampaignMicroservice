namespace OrderProcessing.Domain;

public class Order
{
    public Guid Id { get; set; }
    public Guid CampaignId { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderAmount { get; set; }
    // Additional information
}