using System.Collections.Generic;
using System.Xml.Serialization;

namespace ProductsShop.Models.DTOs
{
    public class UserDto
    {
        [XmlAttribute("name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public int Age { get; set; }

        [XmlAttribute("count")]
        public int Count
        {
            get { return this.Products.Count; }
        }

        [XmlElement("sold-products")]
        public List<ProductDto> Products { get; set; }
    }
}