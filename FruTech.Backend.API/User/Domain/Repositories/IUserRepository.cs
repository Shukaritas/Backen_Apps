using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.User.Domain.Model.Entities;
using UserAggregate = FruTech.Backend.API.User.Domain.Model.Aggregates.User;

namespace FruTech.Backend.API.User.Domain.Repositories;
/// <summary>
///  Repository interface for User aggregate.
/// </summary>
public interface IUserRepository : IBaseRepository<UserAggregate>
{
    Task<UserAggregate?> FindByEmailAsync(string email);
    Task<UserAggregate?> FindByIdentificatorAsync(string identificator);
    Task<Role?> FindRoleByIdAsync(int roleId);
}
