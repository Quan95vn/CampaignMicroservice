using CampaignManagement.Domain;
using MediatR;

namespace CampaignManagement.Application.Query;

public class GetCampaignQuery : IRequest<Campaign>
{
    public Guid Id { get; set; }
}