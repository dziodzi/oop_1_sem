using lab1.Entities;
using lab1.Exceptions;
using lab1.Tools;

namespace lab1.Services;

public class RaceService(double distance, RaceEnums enums, WeatherEnums weather) : IRaceService
{
    private readonly List<Vehicle> _participants = [];

    private void RegisterVehicle(Vehicle transport)
    {
        if ((enums == RaceEnums.Ground && transport is AirVehicle) ||
            (enums == RaceEnums.Air && transport is GroundVehicle))
        {
            throw new InvalidParticipantTypeException(
                $"The {transport.GetType().Name} participant is of an invalid type for {enums} race.");
        }

        _participants.Add(transport);
    }

    public bool RegisterVehicles(List<Vehicle> transports)
    {
        if (transports.Count == 0) return false;
        foreach (var transport in transports)
        {
            RegisterVehicle(transport);
        }

        return true;
    }

    public List<KeyValuePair<Vehicle, double>> StartRace()
    {
        if (_participants.Count == 0)
        {
            throw new NoRaceParticipantsException();
        }

        var results = new Dictionary<Vehicle, double>();

        foreach (var transport in _participants)
        {
            var raceTime = transport.CalculateRaceTime(distance) * (1 / GetWeatherImpact(transport));
            results.Add(transport, raceTime);
        }

        var sortedResults = results.OrderBy(r => r.Value).ToList();

        return sortedResults;
    }

    private double GetWeatherImpact(Vehicle transport)
    {
        return transport switch
        {
            GroundVehicle => weather switch
            {
                WeatherEnums.Rainy => 0.95,
                WeatherEnums.Stormy => 0.9,
                WeatherEnums.Snowy => 0.85,
                _ => 1.0
            },
            AirVehicle => weather switch
            {
                WeatherEnums.Stormy => 0.25,
                WeatherEnums.Snowy => 0.7,
                WeatherEnums.Windy => 0.5,
                WeatherEnums.Rainy => 0.8,
                WeatherEnums.Cloudy => 0.9,
                _ => 1.0
            },
            _ => 1.0
        };
    }

    public RaceEnums GetRaceType()
    {
        return enums;
    }
}