using UserManagement.Domain;

namespace UserManagement.Infrastructure.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
}