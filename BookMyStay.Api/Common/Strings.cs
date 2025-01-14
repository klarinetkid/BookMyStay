namespace BookMyStay.Api.Common
{
    public static class Strings
    {
        public static string IdRequired { get; } = "The ID field is required.";
        public static string IdForbidden { get; } = "The ID field must be empty.";
        public static string ReservationStartBeforeToday { get; } = "Reservation start must be after today.";
        public static string ReservationEndBeforeStart { get; } = "Reservation end cannot be before start.";
        public static string ReservationOverlaps { get; } = "Reservation cannot overlap another reservation.";
        public static string ReservationDurationExceedsLimit
        {
            get
            {
                return $"Reservation must not last longer than {Helper.AppConfig.MaxReservationDurationDays} days.";
            }
        }
        public static string ReservationTooFarAhead
        {
            get
            {
                return $"Reservation end cannot be later than {Helper.AppConfig.MaxReservationDaysAhead} days from today.";
            }
        }

        public static class Exceptions
        {
            public static string IdIsNull { get; } = "Id is null";
        }
    }
}
