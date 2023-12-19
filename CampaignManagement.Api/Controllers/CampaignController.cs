using CampaignManagement.Application.Command;
using CampaignManagement.Application.Query;
using CampaignManagement.Application.Response;
using CampaignManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CampaignManagement.Api.Controllers;

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
    public async Task<ActionResult<List<Campaign>>> GetCampaigns()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCampaignsQuery());
            return Ok(result);
        }
        catch (System.Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
    
    [HttpGet("campaign")]
    public async Task<ActionResult<Campaign>> GetCampaign(Guid campaignId)
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