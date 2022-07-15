using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<int>
{
    public LeaveAllocationDto LeaveAllocationDto { get; set; }
}