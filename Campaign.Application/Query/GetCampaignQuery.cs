using Campaign.Application.Response;
using Campaign.Domain;
using MediatR;

namespace Campaign.Application.Query;

public class GetCampaignQuery : IRequest<CampaignResponse>
{
    public Guid Id { get; set; }
}