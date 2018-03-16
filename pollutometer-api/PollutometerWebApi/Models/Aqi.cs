namespace PollutometerWebApi.Models
{
    public class Aqi
    {
        double _value;
        
        public string GasName { get; set; }
        public double Value
        {
            get 
            {
                return _value;
			}
            set
            {
                _value = value;

                if (_value >= 0 && _value <= 50)
                    Level = "Good";
                else if (_value >= 51 && _value <= 100)
                    Level = "Moderate";
                else if (_value >= 101 && _value <= 150)
                    Level = "Unhealthy for Sensitive Groups";
                else if (_value >= 151 && _value <= 200)
                    Level = "Unhealthy";
                else if (_value >= 201 && _value <= 300)
                    Level = "Very Unhealthy";
                else if (_value >= 301 && _value <= 500)
                    Level = "Hazardous";
            }
        }
        public string Level { get; set; }
    }
}
