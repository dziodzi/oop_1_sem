using lab1.Entities;

namespace lab1.Tools
{
    public static class WeatherEffect
    {
        public static double GetWeatherImpact(Vehicle transport, Weather weather)
        {
            switch (transport)
            {
                case GroundVehicle:
                    switch (weather)
                    {
                        case Weather.Rainy:
                            return 0.95;
                        case Weather.Stormy:
                            return 0.9;
                        case Weather.Snowy:
                            return 0.85;
                        default:
                            return 1.0;
                    }
                    break;
                
                case AirVehicle:
                    switch (weather)
                    {
                        case Weather.Stormy:
                            return 0.25;
                        case Weather.Snowy:
                            return 0.7;
                        case Weather.Windy:
                            return 0.5;
                        case Weather.Rainy:
                            return 0.8;
                        case Weather.Cloudy:
                            return 0.9;
                        default:
                            return 1.0;
                    }
                    break;
                
                default:
                    return 1.0;
            }
        }
    }
}