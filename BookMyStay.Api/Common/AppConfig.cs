namespace BookMyStay.Api.Common
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public int MaxReservationDurationDays { get; set; }
        public int MaxReservationDaysAhead { get; set; }

        public AppConfig(ConfigurationManager config)
        {
            ConnectionString = config.GetConnectionString("Connection") 
                ?? throw new Exception("Connection string not defined");

            MaxReservationDurationDays = config.GetValue<int?>("MaxReservationDurationDays") 
                ?? throw new Exception("MaxReservationDurationDays not defined");

            MaxReservationDaysAhead = config.GetValue<int?>("MaxReservationDaysAhead") 
                ?? throw new Exception("MaxReservationDaysAhead not defined");
        }
    }
}
