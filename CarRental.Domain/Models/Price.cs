using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Models
{
    public class Price
    {
        public double PerDay { get; set; }
        public double PerKm { get; set; }
        public CarCategory CarType { get; set; }
    }
}
