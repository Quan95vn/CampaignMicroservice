namespace Prize.Domain;

public class PrizeClaim
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PrizeId { get; set; }
    public DateTime ClaimDate { get; set; }
}