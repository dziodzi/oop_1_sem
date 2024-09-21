using lab1.Entities;
using lab1.Exceptions;
using lab1.Services;
using lab1.Tools;

namespace lab1;

internal static class Program
{
    private static void Main()
    {
        Actions.OnStartAction();

        var type = Actions.InputRaceTypeAction();

        var distance = Actions.InputRaceDistanceAction();

        var weather = Actions.InputRaceWeatherAction();

        IRaceService raceService = new RaceService(distance, type, weather);

        List<KeyValuePair<Vehicle, double>> result = [];

        while (!raceService.RegisterVehicles(Actions.SelectParticipantsAction(raceService.GetRaceType())))
        {
            try
            {
                result = raceService.StartRace();
            }
            catch (NoRaceParticipantsException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Actions.OutputRaceResultsAction(result);

        Actions.OnFinishAction();
    }
}