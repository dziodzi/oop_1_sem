using System;
using System.Collections.Generic;
using lab1.Entities;
using lab1.Entities.VehicleRealisations;
using lab1.Services;
using lab1.Tools;

namespace lab1;

static class Program
{
    private static void Main()
    {
        Console.WriteLine("Welcome to the Race Simulator!");

        RaceType raceType = GetRaceType();

        double distance = GetPositiveDoubleInput("Enter race distance:");
        
        RaceService raceService = new RaceService(distance, raceType);

        Weather weather = GetWeather();

        List<Vehicle> allVehicles = new List<Vehicle>
        {
            new Centaur(),
            new BabaYagaMortar(),
            new Broom(),
            new Carpet(),
            new BootsOfSpeed(),
            new PumpkinCarriage(),
            new HutOnChickenLegs(),
            new FlyingShip()
        };

        List<Vehicle> availableVehicles = allVehicles.FindAll(t =>
            (raceType == RaceType.Ground && t is GroundVehicle) ||
            (raceType == RaceType.Air && t is AirVehicle) ||
            raceType == RaceType.Mixed);

        Console.WriteLine("Select participants:");
        List<Vehicle> selectedVehicles = new List<Vehicle>();
        for (int i = 0; i < availableVehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableVehicles[i].Name}");
        }

        HashSet<int> selectedParticipants = new HashSet<int>();
        while (true)
        {
            int choice = GetPositiveIntInput("Enter transport number (or 0 to start the race):", 0,
                availableVehicles.Count);
            if (choice == 0) break;
            if (selectedParticipants.Contains(choice))
            {
                Console.WriteLine(
                    $"Be careful! {availableVehicles[choice - 1].Name} already added to the race. I won't add it one more time.");
            }
            else
            {
                selectedVehicles.Add(availableVehicles[choice - 1]);
                selectedParticipants.Add(choice);
                Console.WriteLine($"{availableVehicles[choice - 1].Name} added to the race.");
            }
        }

        try
        {
            foreach (var transport in selectedVehicles)
            {
                raceService.RegisterVehicle(transport);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        foreach (var transport in selectedVehicles)
        {
            double impact = WeatherEffect.GetWeatherImpact(transport, weather);
            Console.WriteLine($"{transport.Name} is affected by {weather} weather: speed multiplier x{impact}");
        }

        raceService.StartRace();
    }

    static RaceType GetRaceType()
    {
        while (true)
        {
            Console.WriteLine("Select race type: 1 - Ground, 2 - Air, 3 - Mixed");
            int raceTypeInput = GetPositiveIntInput("Enter race type:", 1, 3);

            return raceTypeInput switch
            {
                1 => RaceType.Ground,
                2 => RaceType.Air,
                3 => RaceType.Mixed,
                _ => throw new ArgumentException("Invalid race type")
            };
        }
    }

    static Weather GetWeather()
    {
        while (true)
        {
            Console.WriteLine("Enter weather: 1 - Sunny, 2 - Rainy, 3 - Windy, 4 - Cloudy, 5 - Stormy, 6 - Snowy");
            int weatherInput = GetPositiveIntInput("Enter weather type:", 1, 6);

            return weatherInput switch
            {
                1 => Weather.Sunny,
                2 => Weather.Rainy,
                3 => Weather.Windy,
                4 => Weather.Cloudy,
                5 => Weather.Stormy,
                6 => Weather.Snowy,
                _ => throw new ArgumentException("Invalid weather type")
            };
        }
    }

    static double GetPositiveDoubleInput(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (double.TryParse(input, out var result) && result > 0)
                return result;

            Console.WriteLine("Invalid input. Please enter a positive number.");
        }
    }

    static int GetPositiveIntInput(string prompt, int minValue, int maxValue)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out var result) && result >= minValue && result <= maxValue)
                return result;

            Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
        }
    }
}