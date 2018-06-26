using System.Security.Policy;
using ConcertDiary.Models;

namespace ConcertDiary.Services
{
    public interface ITweetService
    {
        string TweetConcert(Concert concert);
    }
}
