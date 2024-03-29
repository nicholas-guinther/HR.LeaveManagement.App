using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // GET: api/<LeaveAllocationsController>
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListRequest());

        return Ok(leaveAllocations);
    }
    
    // GET: api/<LeaveAllocationsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailRequest{Id = id});

        return Ok(leaveAllocation);
    }
    
    // GET: api/<LeaveAllocationsController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto allocation)
    {
        var command = new CreateLeaveAllocationCommand { LeaveAllocationDto = allocation };
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    // PUT api/<LeaveAllocationsController>/5
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto allocation)
    {
        var command = new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = allocation };
        await _mediator.Send(command);
        return NoContent();
    }
    
    // DELETE api/<LeaveAllocationsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand { Id = id };
        await _mediator.Send(command);

        return NoContent();
    }
}