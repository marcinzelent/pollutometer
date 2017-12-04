using PollutometerWebApi.Models;

namespace PollutometerWebApi
{
    public static class AqiCalculator
    {
        public static Aqi CalculateAqi(Reading reading)
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

            double i, c = 0, cLow = 0, cHigh = 0, iLow = 0, iHigh = 0;
            Aqi aqi = new Aqi();
           
            for (int x = 0; x < 4; x++)
            {
                switch (x)
                {
                    case 0:
                        c = reading.Co;
                        break;
                    case 1:
                        c = reading.So;
                        break;
                    case 2:
                        c = reading.No;
                        break;
                }

                for (int y = 0; y < 7; y++)
				{
					if (c >= breakpoints[x, y, 0] && c <= breakpoints[x, y, 1])
					{
						cLow = breakpoints[x, y, 0];
						cHigh = breakpoints[x, y, 1];
						iLow = breakpoints[3, y, 0];
						iHigh = breakpoints[3, y, 1];
                        break;
					}
				}

                i = (iHigh - iLow) / (cHigh - cLow) * (c - cLow) + iLow;

                if (i > aqi.Value)
                { 
                    aqi.Value = i;
                    switch (x)
                    {
                        case 0:
                            aqi.GasName = "CO";
                            break;
                        case 1:
                            aqi.GasName = "SO";
                            break;
                        case 2:
                            aqi.GasName = "NO";
                            break;
                    }
                }
            }

            return aqi;
        }
    }
}
