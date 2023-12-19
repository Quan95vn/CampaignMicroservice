using MediatR;
using UserManagement.Application.Command;
using UserManagement.Application.Response;
using UserManagement.Domain;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Application.CommandHandler;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.UserName,
            Email = request.Email
        };

        await _userRepository.AddUserAsync(user);
        
        return new UserResponse
        {
            Id = user.Id
        };
    }
}