using Campaign.Application.Response;
using MediatR;

namespace Campaign.Application.Query;

public class GetCampaignsQuery : IRequest<List<CampaignResponse>>
{
}