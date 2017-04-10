using System.Xml.Serialization;

namespace ProductsShop.Models.DTOs
{
    [XmlType(TypeName = "sold-products")]
    public class ProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}