using lab1.Entities;

namespace lab1.Services;

public interface IRaceService
{
    public void RegisterVehicles(List<Vehicle> transports);

    public List<KeyValuePair<Vehicle, double>> StartRace();
}