namespace AirPollutionWebApi.Models
{
    public class Reading
    {
        public int Id { get; set; }
        public int TimeStamp { get; set; }
        public double Co { get; set; }
        public double No { get; set; }
        public double So { get; set; }
    }
}
