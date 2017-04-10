using System.Xml.Serialization;

namespace CarDealer.Models.DTOs
{
    [XmlType("part")]
    public class PartDto
    {
        [XmlAttribute("name")]
        public string Part { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}