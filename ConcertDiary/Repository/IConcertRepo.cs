using System;
using ConcertDiary.Models;

namespace ConcertDiary.Repository
{
    public interface IConcertRepo
    {
        Concert[] GetAllConcerts();
        Concert GetConcert(long id);
        Concert SaveConcert(Concert concert);
        Concert UpdateConcert(Concert concert);
        Boolean DeleteConcert(long id);
    }
}