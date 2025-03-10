using Core.Application;
using Core.Application.EventBus;
using Core.Application.EventStore;
using Core.Application.Services;
using User.Application.Services;
using User.Persistence;

namespace User.Infrastructure.Services
{
    public class UserApplicationService(IEventStore<UserDbContext> eventStore, IEventBus eventBusGateway, IUnitOfWork<UserDbContext> unitOfWork) : ApplicationService<UserDbContext>(eventStore, eventBusGateway, unitOfWork), IUserApplicationService { }
}
