using System.ComponentModel.DataAnnotations;

namespace CustomerWebApi.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<CustomerLocation>? Locations { get; set; }

        public Customer()
        {
            Locations = new List<CustomerLocation>();
        }
    }
}
