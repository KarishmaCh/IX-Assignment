using Microsoft.AspNetCore.Mvc;

namespace CustomerWebAPI.Model
{
    public class Customer
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public List<CustomerLocation> Locations { get; set; }


    }
}
