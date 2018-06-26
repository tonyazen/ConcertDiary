
namespace ConcertDiary.Models.ResponseModels
{
    public class ConcertsResponse : ConcertBaseResponse
    {
        public Concert[] Concerts { get; set; }
    }
}