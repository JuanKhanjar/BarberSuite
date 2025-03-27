using System.Text.Json;

namespace BarberSuite.Infrastructure.Persistence.Configuration
{
    public static class JsonSerializationOptions
    {
        public static readonly JsonSerializerOptions Default = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
}
