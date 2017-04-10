using System.Xml.Serialization;

namespace CarDealer.Models.DTOs
{
    [XmlType("customer")]
    public class CustomerDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }
        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }
        [XmlAttribute("spent-money")]
        public decimal MoneySpent { get; set; }
    }
}