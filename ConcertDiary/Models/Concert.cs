using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertDiary.Models
{
    public class Concert
    {
        public long Id { get; set; }
        public Artist Artist { get; set; }
        public Artist SupportingArtist { get; set; }
        public Venue Venue { get; set; }
        public DateTime Date { get; set; }
        public string Seat { get; set; }
        public int Rating { get; set; }
    }
}