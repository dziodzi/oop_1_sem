using System;
using System.Collections.Generic;
using System.Linq;
using lab1.Entities;

namespace lab1.Services;

public class RaceService(double distance, RaceType type)
{
    public double Distance { get; } = distance;
    public RaceType Type { get; } = type;
    private List<Vehicle> Participants { get; } = new List<Vehicle>();

    public void RegisterVehicle(Vehicle transport)
    {
        if ((Type == RaceType.Ground && transport is AirVehicle) ||
            (Type == RaceType.Air && transport is GroundVehicle))
        {
            throw new InvalidOperationException("This type of transport cannot participate in this race!");
        }

        Participants.Add(transport);
    }

    public void StartRace()
    {
        if (Participants.Count == 0)
        {
            Console.WriteLine("No participants registered for the race.");
            return;
        }

        var results = new Dictionary<Vehicle, double>();

        foreach (var transport in Participants)
        {
            double raceTime = transport.CalculateRaceTime(Distance);
            results.Add(transport, raceTime);
        }

        var sortedResults = results.OrderBy(r => r.Value).ToList();

        Console.WriteLine("-------------------------------------\nRace results:");
        for (int i = 0; i < sortedResults.Count; i++)
        {
            var result = sortedResults[i];
            Console.WriteLine($"{i + 1}. {result.Key.Name} - Time: {GetTimeInTimeSpan(result)}");
        }

        var winner = sortedResults.First();
        Console.WriteLine(
            $"-------------------------------------\nWinner: {winner.Key.Name} with time {GetTimeInTimeSpan(winner)}");
    }

    private static string GetTimeInTimeSpan(KeyValuePair<Vehicle, double> keyValuePair)
    {
        TimeSpan timeSpan;
        try {
            timeSpan = TimeSpan.FromSeconds(keyValuePair.Value);
        }
        catch (OverflowException) {
            return "Too much time, dude, I don't want to count";
        }

        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D2}.{4:D3}",
            timeSpan.Days,
            timeSpan.Hours,
            timeSpan.Minutes,
            timeSpan.Seconds,
            timeSpan.Milliseconds);
        return formattedTime;
    }
}