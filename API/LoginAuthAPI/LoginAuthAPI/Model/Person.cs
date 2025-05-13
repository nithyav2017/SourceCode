using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    [Table("person", Schema = "person")]
    public class Person
    {
        [Key]
        
        public int BusinessEntityID { get; set; }
        public string? FirstName { get; set; }
    }
}
