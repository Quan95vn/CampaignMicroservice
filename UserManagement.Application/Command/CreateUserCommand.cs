using MediatR;
using UserManagement.Application.Response;

namespace UserManagement.Application.Command;

public class CreateUserCommand : IRequest<UserResponse>
{
    public string UserName { get; set; }
    public string Email { get; set; }
}