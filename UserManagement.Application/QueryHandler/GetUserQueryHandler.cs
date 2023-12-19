using MediatR;
using UserManagement.Application.Query;
using UserManagement.Application.Response;

namespace UserManagement.Application.QueryHandler;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
{
    public Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}