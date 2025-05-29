using Core.Domain.Primitives;
using User.Domain.Entities;
using User.Domain.Enumerations;
using User.Domain.ValueObjects;
using CreditConsultation = User.Domain.Entities.CreditConsultation;

namespace User.Domain.Aggregates;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Document { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public UserStatus Status { get; private set; }
    public Address Address { get; private set; }
    public DateTimeOffset DateOfBirth { get; private set; }

    public static User Create(Guid UserId, string document, string phone)
    {
        User User = new();

        User.RaiseEvent<DomainEvent.UserCreated>(version => new(
            UserId,
            document,
            phone,
            UserStatus.PendingProfile,
            User.CreatedAt,
            version));

        return User;
    }

    public void UpdateProfile(string name, string email, DateTimeOffset dateOfBirth)
        => RaiseEvent<DomainEvent.ProfileUpdated>(version => new(Id, name, email, UserStatus.Active, dateOfBirth, version));

    public void ChangeStatus(UserStatus UserStatus)
    {
        switch (UserStatus)
        {
            case UserStatus.DefaulterStatus:
                RaiseEvent<DomainEvent.UserDefaulterStatus>(version => new(Id, UserStatus.Defaulter.Name, version));
                break;

            case UserStatus.SaleInProgressStatus:
                RaiseEvent<DomainEvent.UserSaleInProgressStatus>(version => new(Id, UserStatus.SaleInProgress.Name, version));
                break;

            case UserStatus.ActiveStatus:
                RaiseEvent<DomainEvent.UserActiveStatus>(version => new(Id, UserStatus.Active.Name, version));
                break;

            case UserStatus.PendingDebtStatus:
                RaiseEvent<DomainEvent.UserPendingDebtStatus>(version => new(Id, UserStatus.PendingDebt.Name, version));
                break;
        }
    }

    public void UpsertAddress(Dto.Address address)
       => RaiseEvent<DomainEvent.AddressUpserted>(version => new(Id, address, version));

    public void Delete()
        => RaiseEvent<DomainEvent.UserDeleted>(version => new(Id, version));

    protected override void ApplyEvent(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.UserCreated @event)
    {
        Id = @event.UserId;
        Document = @event.Document;
        Phone = @event.Phone;
        Status = (UserStatus)@event.Status;
    }

    private void When(DomainEvent.PaymentProfileCreated @event)
    {
        PaymentProfiles.ForEach(x => x.SetAsNotMain());
        PaymentProfiles.Add(PaymentProfile.Create(@event.PaymentProfileId, @event.ExternalId, @event.GatewayToken));
    }

    private void When(DomainEvent.ProfileUpdated @event)
    {
        Name = @event.Name;
        Email = @event.Email;
        DateOfBirth = @event.DateOfBirth;
        Status = UserStatus.Active;
    }

    private void When(DomainEvent.AddressUpserted @event)
        => Address = Address.Create(@event.Address);

    private void When(DomainEvent.UserDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.UserDefaulterStatus @event)
        => Status = @event.Status;

    private void When(DomainEvent.UserSaleInProgressStatus @event)
        => Status = @event.Status;

    private void When(DomainEvent.UserActiveStatus @event)
        => Status = @event.Status;

    private void When(DomainEvent.UserPendingDebtStatus @event)
        => Status = @event.Status;
}
