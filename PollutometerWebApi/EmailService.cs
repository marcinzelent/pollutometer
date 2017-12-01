using System;
using System.Collections.Generic;
using PollutometerWebApi.Singletons;

namespace PollutometerWebApi
{
    public class EmailService
    {
        public EmailService()
        {
        }

        public static void Start()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(60);

            var timer = new System.Threading.Timer((e) =>
            {
                var command = "SELECT * FROM Readings " +
                                "WHERE TimeStamp IN(SELECT MAX(TimeStamp) FROM Readings)";
                var reading = SqlOperator.GetReadings(command)[0];

                Dictionary<string, double> results = new Dictionary<string, double>();
                results["CO"] = CalculateAqi(reading.Co, "CO");
                results["NO"] = CalculateAqi(reading.No, "NO");
                results["SO"] = CalculateAqi(reading.So, "SO");

				double max = 0;
				string gasName = "";
				foreach (var result in results)
				{
					if (result.Value > max)
					{
						max = result.Value; 
						gasName = result.Key;
					}
				}

                if(max >= 151) EmailSender.SendEmail(gasName, max);
            }, null, startTimeSpan, periodTimeSpan);
        }

        static double CalculateAqi(double c, string t)
        {
            double[,,] breakpoints =
            {
                {
                    {0, 4.4},
                    {4.5, 9.4},
                    {9.5, 12.4},
                    {12.5, 15.4},
                    {15.5, 30.4},
                    {30.5, 40.4},
                    {40.5, 50.4}
                },
                {
                    {0.000, 0.034},
                    {0.035, 0.144},
                    {0.145, 0.224},
                    {0.225, 0.304},
                    {0.305, 0.604},
                    {0.605, 0.804},
                    {0.805, 1.004}
                },
                {
                    {0, 0.05},
                    {0.08, 0.10},
                    {0.15, 0.20},
                    {0.25, 0.31},
                    {0.65, 1.24},
                    {1.25, 1.64},
                    {1.65, 2.04}
                },
                {
                    {0, 50},
                    {51, 100},
                    {101, 150},
                    {151, 200},
                    {201, 300},
                    {301, 400},
                    {401, 500}
                }
            };

            double i, cLow = 0, cHigh = 0, iLow = 0, iHigh = 0;
            int g = 0;

            switch (t)
            {
                case "CO":
                    g = 0;
                    break;
                case "SO":
                    g = 1;
                    break;
                case "NO":
                    g = 2;
                    break;
            }

            for (int j = 0; j < 7; j++)
            {
                if (c >= breakpoints[g, j, 0] && c <= breakpoints[g, j, 1])
                {
                    cLow = breakpoints[g, j, 0];
                    cHigh = breakpoints[g, j, 1];
                    iLow = breakpoints[3, j, 0];
                    iHigh = breakpoints[3, j, 1];
                    break;
                }
            }

            i = (iHigh - iLow) / (cHigh - cLow) * (c - cLow) + iLow;

            return i;
        }
    }
}
