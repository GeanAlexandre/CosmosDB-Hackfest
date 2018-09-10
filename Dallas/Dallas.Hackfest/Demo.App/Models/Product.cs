using System;

namespace Demo.App.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"Product id {Id}, Product Name {Name}";
    }
}
