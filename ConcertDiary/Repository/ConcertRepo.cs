using System;
using System.Linq;
using System.Web;
using ConcertDiary.Models;
using log4net;

namespace ConcertDiary.Repository
{
    public class ConcertRepo : IConcertRepo
    {
        private const string ConcertCacheKey = "ConcertStore";
        private const string ConcertCurrentIdKey = "CurrentId";
        private readonly ILog _logger = LogManager.GetLogger(typeof(ConcertRepo));

        public ConcertRepo()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[ConcertCacheKey] == null)
                {
                    ctx.Cache["CurrentId"] = 2;

                    var concert = new[]
                    {
                        new Concert
                        {
                            Id = 1,
                            Artist = new Artist{Name = "Radiohead"},
                            SupportingArtist = new Artist{Name = "Caribou"},
                            Venue = new Venue{Name = "First Midwest Bank Amp", Address = new Address{ City = "Tinley Park", State = "IL"}},
                            Date = new DateTime(2012, 6, 10),
                            Rating = 5,
                            Seat = "L41078",
                        },
                        new Concert
                        {
                            Id = 2,
                            Artist = new Artist{Name = "Chris Cornell"},
                            SupportingArtist = new Artist{Name = ""},
                            Venue = new Venue{Name = "Devos Performance Hall", Address = new Address{ City = "Grand Rapids", State = "MI"}},
                            Date = new DateTime(2016, 7, 2),
                            Rating = 5,
                            Seat = "MEZZ A35",
                        }
                    };

                    ctx.Cache[ConcertCacheKey] = concert;
                }
            }
        }

        public Concert[] GetAllConcerts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Concert[])ctx.Cache[ConcertCacheKey];
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
                    Seat = "",
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
                    var currentData = ((Concert[])ctx.Cache[ConcertCacheKey]).ToList();
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
                    var currentData = ((Concert[])ctx.Cache[ConcertCacheKey]).ToList();

                    long currentId;
                    Int64.TryParse(ctx.Cache["CurrentId"].ToString(), out currentId);
                    currentId++;
                    concert.Id = currentId;
                    ctx.Cache["CurrentId"] = currentId;

                    currentData.Add(concert);
                    ctx.Cache[ConcertCacheKey] = currentData.ToArray();

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
                    var currentData = ((Concert[])ctx.Cache[ConcertCacheKey]).ToList();
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

                    ctx.Cache[ConcertCacheKey] = currentData.ToArray();

                    return currentData.FirstOrDefault(concert => concert.Id == updateConcert.Id);
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
                    var currentData = ((Concert[])ctx.Cache[ConcertCacheKey]).ToList();
                    var concertToRemove = currentData.FirstOrDefault(concert => concert.Id == id);
                    currentData.Remove(concertToRemove);
                    ctx.Cache[ConcertCacheKey] = currentData.ToArray();

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