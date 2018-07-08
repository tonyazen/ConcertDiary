using System;
using Microsoft.Ajax.Utilities;

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

        public bool ValidateConcert()
        {
            return Artist != null && !Artist.Name.IsNullOrWhiteSpace() &&
                   Venue != null && !Venue.Name.IsNullOrWhiteSpace() &&
                   Date != DateTime.MinValue;
        }
    }
}