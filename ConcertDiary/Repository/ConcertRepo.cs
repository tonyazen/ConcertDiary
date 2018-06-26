using System;
using System.Linq;
using System.Web;
using ConcertDiary.Models;
using log4net;

namespace ConcertDiary.Repository
{
    public class ConcertRepo : IConcertRepo
    {
        private const string CacheKey = "ConcertStore";
        private readonly ILog _logger;

        public ConcertRepo(ILog logger)
        {
            _logger = logger;

            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var concert = new[]
                    {
                        new Concert
                        {
                            Id = 1,
                            Artist = new Artist{Name = ""},
                            SupportingArtist = new Artist{Name = ""},
                            Venue = new Venue{Name = "", Address = new Address{ City = "", State = ""}},
                            Date = DateTime.Today,
                            Rating = 5,
                            Seat = ""
                        },
                        new Concert
                        {
                            Id = 2,
                            Artist = new Artist{Name = ""},
                            SupportingArtist = new Artist{Name = ""},
                            Venue = new Venue{Name = "", Address = new Address{ City = "", State = ""}},
                            Date = DateTime.Today,
                            Rating = 5,
                            Seat = ""
                        }
                    };

                    ctx.Cache[CacheKey] = concert;
                }
            }
        }

        public Concert[] GetAllConcerts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Concert[])ctx.Cache[CacheKey];
            }

            return new[]
            {
                new Concert
                {
                    Id = 0,
                    Artist = new Artist{Name = ""},
                    SupportingArtist = new Artist{Name = ""},
                    Venue = new Venue{Name = "", Address = new Address{ City = "", State = ""}},
                    Date = DateTime.Today,
                    Rating = 5,
                    Seat = ""
                }
            };
        }

        public Concert GetConcert(long id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Concert[])ctx.Cache[CacheKey]).ToList();
                    return currentData.FirstOrDefault(concert => concert.Id == id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"GetConcert Exception. Id: {id}. Exception: {ex}");
                    return null;
                }
            }

            return null;
        }

        public Concert SaveConcert(Concert concert)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Concert[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(concert);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return concert;

                }
                catch (Exception ex)
                {
                    _logger.Error($"SaveConcert Exception. Concert: Artist:{concert.Artist}, Supporting Artist:{concert.SupportingArtist}, Venue:{concert.Venue}, Seat:{concert.Seat}, Date:{concert.Date}, Rating:{concert.Rating}. Exception: {ex}");
                }
            }

            return null;
        }

        public Concert UpdateConcert(Concert updateConcert)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Concert[])ctx.Cache[CacheKey]).ToList();
                    foreach (var concert in currentData)
                    {
                        if (concert.Id == updateConcert.Id)
                        {
                            concert.Artist = updateConcert.Artist;
                            concert.SupportingArtist = updateConcert.SupportingArtist;
                            concert.Venue = updateConcert.Venue;
                            concert.Seat = updateConcert.Seat;
                            concert.Date = updateConcert.Date;
                            concert.Rating = concert.Rating;
                        }
                    }

                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return currentData.FirstOrDefault(beer => beer.Id == updateConcert.Id);
                }
                catch (Exception ex)
                {
                    _logger.Error($"UpdateConcert Exception. Id:{updateConcert.Id}, Concert: Artist:{updateConcert.Artist}, Supporting Artist:{updateConcert.SupportingArtist}, Venue:{updateConcert.Venue}, Seat:{updateConcert.Seat}, Date:{updateConcert.Date}, Rating:{updateConcert.Rating}. Exception: {ex}");
                    return updateConcert;
                }
            }

            return null;
        }

        public bool DeleteConcert(long id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Concert[])ctx.Cache[CacheKey]).ToList();
                    var concertToRemove = currentData.FirstOrDefault(concert => concert.Id == id);
                    currentData.Remove(concertToRemove);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error($"DeleteConcert Exception. Id: {id}. Exception: {ex}.");
                    return false;
                }
            }

            return false;
        }
    }
}