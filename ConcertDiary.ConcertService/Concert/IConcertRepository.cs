using System;

namespace ConcertDiary.ConcertService.Concert
{
    public interface IConcertRepository
    {
        void SaveConcert(Models.Concert concert);
    }
}
