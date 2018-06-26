using System.Configuration;

namespace ConcertDiary.Configuration
{
    public static class ConfigurationValues
    {
        private static string _userIdHeader;
        public static string UserIdHeader
        {
            get => _userIdHeader ?? (_userIdHeader = "user");
            set => _userIdHeader = value;
        }

        private static string _appIdHeader;
        public static string AppIdHeader
        {
            get => _appIdHeader ?? (_appIdHeader = "app_id");
            set => _appIdHeader = value;
        }
    }
}