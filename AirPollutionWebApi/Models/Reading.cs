namespace AirPollutionWebApi.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public int TimeStamp { get; set; }
        public decimal Co { get; set; }
        public decimal No { get; set; }
        public decimal So { get; set; }
    }
}
