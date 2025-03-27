namespace BarberSuite.Domain.Models
{
    public class Service
    {
        // Private constructor to enforce validation
        private Service() { }

        // Public factory method for controlled creation
        public static Service Create(
            string name,
            decimal price,
            ServiceCategory category)
        {
            ValidateName(name);
            ValidatePrice(price);
            return new Service
            {
                Id = Guid.NewGuid(),
                Name = name,
                BasePrice = new Money(price, Currency.DKK), // Using value object
                Category = category,
                IsActive = true
            };
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Money BasePrice { get; private set; } // Value object
        public ServiceCategory Category { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? LastModifiedAt { get; private set; }

        // Domain Methods
        public void ApplyDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Invalid discount percentage");

            if (!Category.AllowsDiscounts)
                throw new InvalidOperationException("Discounts not allowed for this category");

            BasePrice = BasePrice.ApplyDiscount(percentage);
            LastModifiedAt = DateTimeOffset.UtcNow;
        }

        public void UpdatePricing(Money newPrice)
        {
            if (newPrice.Amount < BasePrice.Amount * 0.5m)
                throw new InvalidOperationException("Price cannot be reduced by more than 50%");

            BasePrice = newPrice;
            LastModifiedAt = DateTimeOffset.UtcNow;
        }

        // Private validation methods
        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                throw new ArgumentException("Invalid service name");
        }

        private static void ValidatePrice(decimal price)
        {
            if (price < 50 || price > 2000)
                throw new ArgumentException("Price must be between 50 and 2000");
        }

        private static void ValidateDuration(TimeSpan duration)
        {
            if (duration < TimeSpan.FromMinutes(15) || duration > TimeSpan.FromHours(3))
                throw new ArgumentException("Invalid service duration");
        }

        // Value type for money
        public record Money(decimal Amount, Currency Currency)
        {
            public Money ApplyDiscount(decimal percentage) =>
                this with { Amount = Amount * (1 - percentage / 100) };
        }

        public enum Currency { DKK, EUR, USD }

        // Enhanced category with behavior
        public class ServiceCategory
        {
            public static readonly ServiceCategory Haircut = new(1, "Haircut", allowsDiscounts: true);
            public static readonly ServiceCategory Coloring = new(2, "Coloring", allowsDiscounts: false);

            public int Id { get; }
            public string Name { get; }
            public bool AllowsDiscounts { get; }
            public QualificationLevel RequiredQualifications { get; }

            private ServiceCategory(int id, string name, bool allowsDiscounts)
            {
                Id = id;
                Name = name;
                AllowsDiscounts = allowsDiscounts;
                RequiredQualifications = id switch
                {
                    1 => QualificationLevel.Basic,
                    2 => QualificationLevel.Advanced,
                    _ => QualificationLevel.None
                };
            }
        }

        public enum QualificationLevel { None, Basic, Advanced }
    }
}
