using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;
using lab1.Exceptions;
using lab1.Tools;

namespace lab1.Services;

public class RaceService(double distance, RaceType type, WeatherType weather)
{
    public double Distance { get; } = distance;
    public RaceType Type { get; } = type;
    public WeatherType Weather { get; } = weather;
    private List<Vehicle> Participants { get; } = new List<Vehicle>();

    public void RegisterVehicle(Vehicle transport)
    {
        if ((Type == RaceType.Ground && transport is AirVehicle) ||
            (Type == RaceType.Air && transport is GroundVehicle))
        {
            throw new InvalidParticipantTypeException(
                $"The {transport.GetType().Name} participant is of an invalid type for {Type} race.");
        }

        Participants.Add(transport);
    }

    public void RegisterVehicles(List<Vehicle> transports)
    {
        foreach (Vehicle transport in transports)
        {
            RegisterVehicle(transport);
        }
    }

    public List<KeyValuePair<Vehicle, double>> StartRace()
    {
        if (Participants.Count == 0)
        {
            throw new NoRaceParticipantsException();
        }

        var results = new Dictionary<Vehicle, double>();

        foreach (var transport in Participants)
        {
            double raceTime = transport.CalculateRaceTime(Distance) * (1 / GetWeatherImpact(transport));
            results.Add(transport, raceTime);
        }

        var sortedResults = results.OrderBy(r => r.Value).ToList();

        return sortedResults;
    }

    private double GetWeatherImpact(Vehicle transport)
    {
        switch (transport)
        {
            case GroundVehicle:
                switch (Weather)
                {
                    case WeatherType.Rainy:
                        return 0.95;
                    case WeatherType.Stormy:
                        return 0.9;
                    case WeatherType.Snowy:
                        return 0.85;
                    default:
                        return 1.0;
                }

                break;

            case AirVehicle:
                switch (Weather)
                {
                    case WeatherType.Stormy:
                        return 0.25;
                    case WeatherType.Snowy:
                        return 0.7;
                    case WeatherType.Windy:
                        return 0.5;
                    case WeatherType.Rainy:
                        return 0.8;
                    case WeatherType.Cloudy:
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