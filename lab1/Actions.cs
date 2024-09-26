using lab1.Entities;
using lab1.Entities.VehicleRealisations;
using lab1.Services;
using lab1.Tools;

namespace lab1;

public abstract class Actions
{
    public static void OnStartAction()
    {
        Console.WriteLine("Welcome to the Race Simulator!");
    }

    public static RaceEnums InputRaceTypeAction()
    {
        while (true)
        {
            Console.WriteLine("Select race type: 1 - Ground, 2 - Air, 3 - Mixed");
            var raceTypeInput = GetPositiveIntInput("Enter race type:", 1, 3);

            return raceTypeInput switch
            {
                1 => RaceEnums.Ground,
                2 => RaceEnums.Air,
                3 => RaceEnums.Mixed,
                _ => throw new ArgumentException("Invalid race type")
            };
        }
    }

    public static double InputRaceDistanceAction()
        {
            while (true)
            {
                var distance = GetPositiveDoubleInput("Enter race distance: ");
                return distance;
            }
        }

    public static WeatherEnums InputRaceWeatherAction()
    {
        while (true)
        {
            Console.WriteLine("Enter weather: 1 - Sunny, 2 - Rainy, 3 - Windy, 4 - Cloudy, 5 - Stormy, 6 - Snowy");
            var weatherInput = GetPositiveIntInput("Enter weather type:", 1, 6);

            return weatherInput switch
            {
                1 => WeatherEnums.Sunny,
                2 => WeatherEnums.Rainy,
                3 => WeatherEnums.Windy,
                4 => WeatherEnums.Cloudy,
                5 => WeatherEnums.Stormy,
                6 => WeatherEnums.Snowy,
                _ => throw new ArgumentException("Invalid weather type")
            };
        }
    }

    public static List<Vehicle> SelectParticipantsAction(RaceEnums raceEnums)
    {
        var selectedVehicles = new List<Vehicle>();
        List<Vehicle> vehicles =
        [
            new Centaur(),
            new BabaYagaMortar(),
            new Broom(),
            new Carpet(),
            new BootsOfSpeed(),
            new PumpkinCarriage(),
            new HutOnChickenLegs(),
            new FlyingShip()
        ];

        vehicles = vehicles.FindAll(t =>
            (raceEnums == RaceEnums.Ground && t is GroundVehicle) ||
            (raceEnums == RaceEnums.Air && t is AirVehicle) ||
            raceEnums == RaceEnums.Mixed);

        Console.WriteLine("Select participants:");

        for (var i = 0; i < vehicles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {vehicles[i].Name}");
        }

        while (true)
        {
            var input = GetPositiveIntInput("Enter transport number (or 0 to start the race):", 0,
                vehicles.Count);
            if (input == 0) return selectedVehicles;

            if (selectedVehicles.Contains(vehicles[input - 1]))
            {
                Console.WriteLine($"Can't add {vehicles[input - 1].Name} to the race because it is already added.");
                continue;
            }

            selectedVehicles.Add(vehicles[input - 1]);
            Console.WriteLine($"{vehicles[input - 1].Name} added to the race.");
        }
    }

    public static void OutputRaceResultsAction(List<KeyValuePair<Vehicle, double>> results)
    {
        Console.WriteLine("-------------------------------------\nRace results:");
        for (var i = 0; i < results.Count; i++)
        {
            var result = results[i];
            Console.WriteLine($"{i + 1}. {result.Key.Name} - Time: {GetTimeInTimeSpan(result)}");
        }

        var winner = results.First();
        Console.WriteLine(
            $"-------------------------------------\nWinner: {winner.Key.Name} with time {GetTimeInTimeSpan(winner)}");
    }

    public static void OnFinishAction()
    {
        Console.WriteLine("-------------------------------------\nThat's all, folks!");
    }

    private static int GetPositiveIntInput(string prompt, int minValue, int maxValue)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var result) && result >= minValue && result <= maxValue)
                return result;

            Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
        }
    }

    private static double GetPositiveDoubleInput(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (double.TryParse(input, out var result) && result > 0)
                return result;

            Console.WriteLine("Invalid input. Please enter a positive number.");
        }
    }

    private static string GetTimeInTimeSpan(KeyValuePair<Vehicle, double> keyValuePair)
    {
        TimeSpan timeSpan;
        try
        {
            timeSpan = TimeSpan.FromSeconds(keyValuePair.Value);
        }
        catch (OverflowException)
        {
            return "Literally Math.pow(Slowpoke, infinity) case";
        }

        var formattedTime =
            $"{timeSpan.Days:D2}:{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}.{timeSpan.Seconds:D2}.{timeSpan.Milliseconds:D3}";
        return formattedTime;
    }
}