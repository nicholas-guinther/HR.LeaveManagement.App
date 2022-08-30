using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "5dddc548-4a6b-461b-a74c-444e78e64d98",
                UserId = "d8d25874-6d69-43cb-916b-8411ffd155f8"
            },
            new IdentityUserRole<string>
            {
                RoleId = "79c1d3d2-ef13-4b3e-bc01-2428b2f472c6",
                UserId = "372e2c2d-9d5d-4e0f-bb9f-8ecbbd520fb7"
            }
            );
    }
}