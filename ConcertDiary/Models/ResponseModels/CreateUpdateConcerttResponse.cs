
namespace ConcertDiary.Models.ResponseModels
{
    public class CreateUpdateConcertResponse : ConcertBaseResponse
    {
        public Concert Concert { get; set; }
        public string TweetUrl { get; set; }
    }
}