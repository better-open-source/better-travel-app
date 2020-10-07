using System;
using System.Collections.Generic;

namespace BetterTravel.App
{
    public class Envelop<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TimeGenerated { get; set; }
    }

    public class PagedData<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
    }

    public class HotTourViewModel
    {
        public string Name { get; set; }
        public int HotelCategory { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Departure { get; set; }
        public string DetailsLink { get; set; }
        public int Duration { get; set; }
        public string ImageLink { get; set; }
        public string Price { get; set; }
        public string Country { get; set; }
        public string Resort { get; set; }
    }
}
