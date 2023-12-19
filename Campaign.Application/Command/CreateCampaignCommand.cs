using Campaign.Application.Response;
using MediatR;

namespace Campaign.Application.Command;

public class CreateCampaignCommand : IRequest<CampaignResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int PrizeCount { get; set; }
}