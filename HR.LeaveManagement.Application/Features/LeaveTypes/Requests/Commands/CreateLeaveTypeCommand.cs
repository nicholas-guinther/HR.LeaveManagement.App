using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;

// returns Id of the new record
public class CreateLeaveTypeRequest : IRequest<int>
{
    public LeaveTypeDto LeaveTypeDto { get; set; }
}