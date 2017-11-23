using System;

namespace AirPollutionWebApi.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public int TimeStamp { get; set; }
        public int Co { get; set; }
        public int No { get; set; }
        public int So { get; set; }
    }
}
