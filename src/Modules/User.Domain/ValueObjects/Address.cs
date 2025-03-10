namespace User.Domain.ValueObjects
{
    public record Address(string Street, string City, string State, string District, string ZipCode, string Country, string? Number, string Complement)
    {
        public static Address Create(Dto.Address dto)
            => new(dto?.Street, dto?.City, dto?.State, dto?.District, dto?.ZipCode, dto?.Country, dto?.Number, dto?.Complement);

        public static implicit operator Dto.Address(Address address)
            => new(address?.Street, address?.City, address?.State, address?.District, address?.ZipCode, address?.Country, address?.Number, address?.Complement);

        public static bool operator ==(Address address, Dto.Address dto)
            => dto == (Dto.Address)address;

        public static bool operator !=(Address address, Dto.Address dto)
            => dto != (Dto.Address)address;

        public static Address Undefined
            => new("Undefined", "Undefined", "Undefined", "Undefined", "Undefined", "Undefined", "Undefined", "Undefined");

        public static implicit operator Address(Dto.Address address)
            => new(address.Street, address.City, address.State, address.District, address.ZipCode, address.Country, address.Number, address.Complement);
    }
}
