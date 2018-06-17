using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string SSN { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        public int StartOfCurrentMeter { get; set; }
        public int EndofCurrentMeter { get; set; }
        public CarCategory CarCategory { get; set; }
        public double Price { get; set; }
    }
}
