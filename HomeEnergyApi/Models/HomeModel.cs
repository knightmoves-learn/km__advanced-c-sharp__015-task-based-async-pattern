using System.ComponentModel.DataAnnotations;
namespace HomeEnergyApi.Models
{
    public class Home
    {
        [Required]
        public string OwnerLastName { get; }

        [StringLength(40)]
        public string? StreetAddress { get; }

        public string? City { get; }

        [Range(0, 50000, ErrorMessage = "Monthly electric usage is limited to positive numbers of 50,000 kWh or less")]
        public int? MonthlyElectricUsage { get; }

        public Home(string ownerLastName, string? streetAddress, string? city, int? monthlyElectricUsage)
        {
            OwnerLastName = ownerLastName;
            StreetAddress = streetAddress;
            City = city;
            MonthlyElectricUsage = monthlyElectricUsage;
        }
    }
}