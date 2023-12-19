namespace Eligibility.Domain;

public class Eligibility
{
    public Guid UserId { get; set; }
    public bool IsEligible { get; set; }
    public decimal TotalSpent { get; set; }
    public int PrizesClaimed { get; set; }
    public DateTime LastClaimDate { get; set; }
}