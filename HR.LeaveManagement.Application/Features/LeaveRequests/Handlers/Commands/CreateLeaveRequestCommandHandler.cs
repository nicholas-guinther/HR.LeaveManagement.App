using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IEmailSender emailSender,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _emailSender = emailSender;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(command.CreateLeaveRequestDto);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        var leaveRequest = _mapper.Map<LeaveRequest>(command.CreateLeaveRequestDto);

        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);

        response.Success = true;
        response.Message = "Creation Successful";
        response.Id = leaveRequest.Id;
        var email = new Email
        {
            To = "employee@org.com",
            Body =
                $"Your leave request for {command.CreateLeaveRequestDto.StartDate} to {command.CreateLeaveRequestDto.EndDate}" +
                $"has been submitted successfully.",
            Subject = "Leave Request Submitted"
        };
        try
        {
            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            
        }

        return response;
    }
}