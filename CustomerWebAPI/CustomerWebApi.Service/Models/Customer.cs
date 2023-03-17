using System.ComponentModel.DataAnnotations;

namespace CustomerWebApi.Model
{
    public class Customer
    {/*
        public int? CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        */

        public int Id { get; set; }
        public string? Name { get; set; }
        public List<CustomerLocation>? Locations { get; set; }

        public Customer()
        {
            Locations = new List<CustomerLocation>();
        }
    }
}
