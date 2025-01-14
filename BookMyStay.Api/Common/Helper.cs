using BookMyStay.DataServices;

namespace BookMyStay.Api.Common
{
    public class Helper
    {
        public static DateOnly NowDateOnly
        {
            get
            {
                return DateOnly.FromDateTime(DateTime.Now);
            }
        }

        public static AppConfig AppConfig 
        { 
            get
            {
                if (_appConfig == null) throw new Exception("AppConfig is null");
                return _appConfig;
            }
            set
            {
                _appConfig = value;
            }
        }

        private static AppConfig? _appConfig;

        public static ApplicationDbContext GetDbContext()
        {
            return new ApplicationDbContext(AppConfig.ConnectionString);
        }
    }
}
