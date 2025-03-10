using Core.Application.Services;
using User.Persistence;

namespace User.Application.Services
{
    public interface IUserApplicationService : IApplicationService<UserDbContext> { }
}
