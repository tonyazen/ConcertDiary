using ConcertDiary.Models;
using ConcertDiary.Models.ResponseModels;

namespace ConcertDiary.Services
{
    public interface IConcertService
    {
        ConcertsResponse GetConcerts(string requestId);
        ConcertResponse GetConcert(string requestId, long concertId);
        CreateUpdateConcertResponse CreateConcert(string requestId, Concert concert);
        CreateUpdateConcertResponse UpdateConcert(string requestId, Concert concert);
        RemoveConcertResponse RemoveConcert(string requestId, long concertId);
    }
}
