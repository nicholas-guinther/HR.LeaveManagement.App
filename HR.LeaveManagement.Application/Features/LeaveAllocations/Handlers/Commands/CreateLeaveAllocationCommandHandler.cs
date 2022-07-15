using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateLeaveAllocationCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(command.LeaveAllocationDto);

        if (validationResult.IsValid == false)
        {
            throw new ValidationException(validationResult);
        }
        var leaveAllocation = _mapper.Map<LeaveAllocation>(command.LeaveAllocationDto);

        leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);

        return leaveAllocation.Id;
    }
}