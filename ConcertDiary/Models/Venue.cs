using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertDiary.Models
{
    public class Venue
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    }
}