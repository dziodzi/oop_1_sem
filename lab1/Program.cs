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
        
        var raceService = new RaceService(distance, type, weather);

        raceService.RegisterVehicles(Actions.SelectParticipantsAction(raceService.GetRaceType()));
        
        var result = raceService.StartRace();

        Actions.OutputRaceResultsAction(result);

        Actions.OnFinishAction();
    }
}