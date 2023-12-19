using MediatR;
using UserManagement.Application.Response;
using UserManagement.Domain;

namespace UserManagement.Application.Query;

public class GetUserQuery : IRequest<UserResponse>
{
    public Guid Id { get; set; }
}