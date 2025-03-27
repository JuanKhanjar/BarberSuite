namespace BarberSuite.Domain.Models.Shops
{
    public class ShopSearchCriteria
    {
        public string? Cvr { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
    }

}
