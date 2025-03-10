namespace User.Domain.Exceptions
{
    public class EstablishmentNotFoundException(Guid establishmentId) : Exception($"Establishment with establishmentId '{establishmentId}' not found.") { }
}
