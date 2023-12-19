namespace Prize.Domain;

public class Prize
{
    public Guid Id { get; set; }
    public string Name { get; set; } // e.g., "Free Pokemon", "Free T-Shirt"
    public decimal TotalQuantity { get; set; }
    public int ClaimedQuantity { get; set; }
    public Guid CampaignId { get; set; }
}