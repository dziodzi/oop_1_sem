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
        Actions.OnStartAction();

        RaceType type = Actions.InputRaceTypeAction();
        
        double distance = Actions.InputRaceDistanceAction();

        WeatherType weather = Actions.InputRaceWeatherAction();
        
        RaceService raceService = new RaceService(distance, type, weather);

        raceService.RegisterVehicles(Actions.SelectParticipantsAction(raceService.Type));
        
        var result = raceService.StartRace();

        Actions.OutputRaceResultsAction(result);

        Actions.OnFinishAction();
    }
}