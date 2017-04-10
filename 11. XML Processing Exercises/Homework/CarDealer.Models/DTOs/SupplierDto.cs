using System.Xml.Serialization;

namespace CarDealer.Models.DTOs
{
    [XmlType("suplier")]
    public class SupplierDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}