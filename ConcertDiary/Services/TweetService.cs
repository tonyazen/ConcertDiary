using ConcertDiary.Models;

namespace ConcertDiary.Services
{
    public class TweetService : ITweetService
    {
        public string TweetConcert(Concert concert)
        {
            return "https://twitter.com/TheRyanAdams/status/1010721868452524032";
        }
    }
}