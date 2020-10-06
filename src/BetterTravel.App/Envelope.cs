using System;

namespace BetterTravel.App
{
    public class Envelope<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public DateTimeOffset TimeGenerated { get; set; }
    }

    public class HotTourViewModel
    {
        public string Name { get; set; }
        public int HotelCategory { get; set; }
        public Uri ImageLink { get; set; }
        public Uri DetailsLink { get; set; }
        public int DurationCount { get; set; }
        public int DurationType { get; set; }
        public int DepartureLocationId { get; set; }
        public DateTimeOffset DepartureDate { get; set; }
        public double PriceAmount { get; set; }
        public int PriceType { get; set; }
        public int PriceCurrencyId { get; set; }
        public int CountryId { get; set; }
        public string ResortName { get; set; }
    }
}
