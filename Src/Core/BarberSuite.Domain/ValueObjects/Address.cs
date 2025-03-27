using System.Text.RegularExpressions;

namespace BarberSuite.Domain.ValueObjects
{
    public sealed class Address
    {
        public string Street { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string Country { get; }

        public Address(string street, string city, string postalCode, string country = "Denmark")
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Street)) throw new ArgumentException("Street required");
            if (!Regex.IsMatch(PostalCode, @"^\d{4}$")) throw new ArgumentException("Invalid DK postal code");
        }

        public override string ToString() => $"{Street}, {PostalCode} {City}";
    }
}
