using System.Xml.Serialization;

namespace ProductsShop.Models.DTOs
{
    [XmlType("products-of-user")]
    public class UserAndProductsDto
    {
        [XmlElement("user")]
        public UserDto UserDto { get; set; }

    }
}