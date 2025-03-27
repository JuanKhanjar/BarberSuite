using BarberSuite.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BarberSuite.Domain.Models.Shops
{
    public class Shop
    {
        public Guid Id { get; } = Guid.NewGuid();

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "CVR must be 8 digits")]
        public required string Cvr { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required Address Address { get; set; }

        [Phone]
        public required string Phone { get; set; }

        public List<Service> Services { get; } = new();
        public Dictionary<DayOfWeek, (TimeSpan Open, TimeSpan Close)> OpeningHours { get; set; }= new();
            
    }
}
