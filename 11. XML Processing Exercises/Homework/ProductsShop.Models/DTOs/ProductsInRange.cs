using System.Xml.Serialization;

namespace ProductsShop.Models.DTOs
{
    public class ProductsInRange
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }
        [XmlAttribute("buyer")]
        public string Buyer { get; set; }
    }
}