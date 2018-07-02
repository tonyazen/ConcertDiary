using ConcertDiary.Controllers;
using ConcertDiary.Models;
using ConcertDiary.Models.ResponseModels;
using ConcertDiary.Repository;
using log4net;
using Exception = System.Exception;

namespace ConcertDiary.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepo _concertRepo;
        private readonly ILog _logger = LogManager.GetLogger((typeof(ConcertsController)));

        public ConcertService(IConcertRepo concertRepo)
        {
            _concertRepo = concertRepo;
        }

        public ConcertsResponse GetConcerts(string requestId)
        {
            var response = new ConcertsResponse
            {
                RequestId = requestId
            };

            try
            {
                response.Concerts = _concertRepo.GetAllConcerts();
            }
            catch(Exception ex)
            {
                _logger.Error($"GetConcerts Exception. Request Id:{requestId}. Exception: {ex}");
            }

            return response;
        }

        public ConcertResponse GetConcert(string requestId, long concertId)
        {
            var response = new ConcertResponse
            {
                RequestId = requestId,
            };

            try
            {
                response.Concert = _concertRepo.GetConcert(concertId);
            }
            catch (Exception ex)
            {
                _logger.Error($"GetConcert Exception. Request Id:{requestId}, Concert Id:{concertId}. Exception: {ex}");
            }

            return response;
        }

        public CreateUpdateConcertResponse CreateConcert(string requestId, Concert concert)
        {
            var response = new CreateUpdateConcertResponse
            {
                RequestId = requestId,
            };

            try
            {
                response.Concert = _concertRepo.SaveConcert(concert);
            }
            catch (Exception ex)
            {
                _logger.Error($"CreateConcert Exception. Concert: Request Id:{requestId}, Artist:{concert.Artist}, Supporting Artist:{concert.SupportingArtist}, Venue:{concert.Venue}, Seat:{concert.Seat}, Date:{concert.Date}, Rating:{concert.Rating}. Exception: {ex}");
            }

            return response;
        }

        public CreateUpdateConcertResponse UpdateConcert(string requestId, Concert concert)
        {
            var response = new CreateUpdateConcertResponse
            {
                RequestId = requestId,
            };

            try
            {
                response.Concert = _concertRepo.UpdateConcert(concert);
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateConcert Exception. Request Id:{requestId}, Concert Id:{concert.Id}, Concert: Artist:{concert.Artist}, Supporting Artist:{concert.SupportingArtist}, Venue:{concert.Venue}, Seat:{concert.Seat}, Date:{concert.Date}, Rating:{concert.Rating}. Exception: {ex}");
            }

            return response;
        }

        public RemoveConcertResponse RemoveConcert(string requestId, long concertId)
        {
            var response = new RemoveConcertResponse
            {
                RequestId = requestId,
            };

            try
            {
                response.Removed = _concertRepo.DeleteConcert(concertId);
            }
            catch (Exception ex)
            {
                _logger.Error($"RemoveConcert Exception. Request Id:{requestId}, Concert Id:{concertId}. Exception: {ex}.");
            }

            return response;
        }
    }
}