using System.Xml.Serialization;

namespace ProductsShop.Models.DTOs
{
    [XmlType("category")]
    public class CategoryDto
    {
        [XmlAttribute("name")]
        public string Category { get; set; }
        [XmlElement("products-count")]
        public int? Count { get; set; }
        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }
        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}