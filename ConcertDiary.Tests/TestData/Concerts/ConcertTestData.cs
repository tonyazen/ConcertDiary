using System;
using System.Diagnostics.CodeAnalysis;
using ConcertDiary.Models;

namespace ConcertDiary.Tests.TestData.Concerts
{
    [ExcludeFromCodeCoverage]
    public static class ConcertTestData
    {
        public static Concert[] GetAllConcerts()
        {
            var concerts = new Concert[3];
            concerts[0] = new Concert
            {
                Id = 1234,
                Artist = new Artist { Name = "Test Artist 1"},
                SupportingArtist = new Artist { Name = "Supporting Artist 1" },
                Venue = new Venue() { Name = "Test Venue 1", Address = new Address { City= "Detroit", State = "MI"} },
                Seat = "Test Seat 1",
                Date = DateTime.Today,
                Rating = 5
            };
            concerts[1] = new Concert
            {
                Id = 1235,
                Artist = new Artist { Name = "Test Artist 2" },
                SupportingArtist = new Artist { Name = "Supporting Artist 2" },
                Venue = new Venue() { Name = "", Address = new Address { City = "Detroit", State = "MI" } },
                Seat = "Test Seat 2",
                Date = DateTime.Today,
                Rating = 5
            };
            concerts[2] = new Concert
            {
                Id = 1236,
                Artist = new Artist { Name = "Test Artist 3" },
                SupportingArtist = new Artist { Name = "Supporting Artist 3" },
                Venue = new Venue() { Name = "", Address = new Address { City = "Detroit", State = "MI" } },
                Seat = "Test Seat 3",
                Date = DateTime.Today,
                Rating = 5
            };

            return concerts;
        }

        public static Concert GetConcertToCreate()
        {
            return new Concert
            {
                Artist = new Artist { Name = "Test Artist" },
                SupportingArtist = new Artist { Name = "Test Supporting Artist" },
                Venue = new Venue { Name = "Test Venue", Address = new Address { City = "Detroit", State = "MI" } },
                Date = new DateTime(2018, 7, 13),
                Seat = "Test Seat",
                Rating = 5
            };
        }

        public static Concert GetCreatedConcert()
        {
            var concert = GetConcertToCreate();
            concert.Id = 1237;
            return concert;
        }
    }
}
