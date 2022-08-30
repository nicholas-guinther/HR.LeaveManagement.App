using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly CreateLeaveTypeDto _leaveTypeDto;
    private readonly CreateLeaveTypeCommandHandler _handler;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

        _leaveTypeDto = new CreateLeaveTypeDto
        {
            DefaultDays = 15,
            Name = "Test DTO"
        };
    }
    
    [Fact]
    public async Task Valid_leaveType_Added()
    {
        var result = await _handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _leaveTypeDto },
            CancellationToken.None);

        var leaveTypes = await _mockRepo.Object.GetAll();

        result.ShouldBeOfType<BaseCommandResponse>();

        leaveTypes.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_LeaveType_Added()
    {
        _leaveTypeDto.DefaultDays = -1;
        
        var result = await _handler.Handle(new CreateLeaveTypeCommand() { CreateLeaveTypeDto = _leaveTypeDto },
            CancellationToken.None);             
        
        var leaveTypes = await _mockRepo.Object.GetAll();
        
        leaveTypes.Count.ShouldBe(2);
        
        result.ShouldBeOfType<BaseCommandResponse>();
    }
}