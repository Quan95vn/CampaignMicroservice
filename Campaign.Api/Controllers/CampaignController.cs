using Campaign.Application.Command;
using Campaign.Application.Query;
using Campaign.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Campaign.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("campaigns")]
    public async Task<ActionResult<List<Domain.Campaign>>> GetCampaigns()
    {
        try
        {
            var result = await _mediator.Send(new GetCampaignsQuery());
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    
    [HttpGet("campaign")]
    public async Task<ActionResult<Domain.Campaign>> GetCampaign(Guid campaignId)
    {
        try
        {
            var result = await _mediator.Send(new GetCampaignQuery{ Id = campaignId });
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<CampaignResponse>> CreateCampaign(CreateCampaignCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}