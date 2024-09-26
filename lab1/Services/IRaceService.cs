using lab1.Entities;
using lab1.Tools;

namespace lab1.Services;

public interface IRaceService
{
    public bool RegisterVehicles(List<Vehicle> transports);

    public List<KeyValuePair<Vehicle, double>> StartRace();

    public RaceEnums GetRaceType();
}