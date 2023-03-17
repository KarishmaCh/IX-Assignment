using System.ComponentModel.DataAnnotations;

namespace CustomerWebApi.Model
{
 public class CustomerLocation
{
      

        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Name { get; set; }
        
        public int CustomerId { get; set; }

    }
}
