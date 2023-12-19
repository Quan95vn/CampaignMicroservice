using CampaignManagement.Domain;
using MediatR;

namespace CampaignManagement.Application.Query;

public class GetAllCampaignsQuery : IRequest<List<Campaign>>
{
}