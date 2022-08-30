using System;
using System.Collections.Generic;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Vacation",
                CreatedBy = "Admin",
                LastModifiedBy = "Admin",
                DateCreated = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            },
            new LeaveType
            {
                Id = 2,
                DefaultDays = 12,
                Name = "Sick",
                CreatedBy = "Admin",
                LastModifiedBy = "Admin",
                DateCreated = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return leaveType;
        });
        return mockRepo;
    }
}