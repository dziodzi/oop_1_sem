namespace lab1.Entities.VehicleRealisations;

internal class Centaur() : GroundVehicle("Centaur", 35.0, 75.0)
{
    protected override double GetRestDuration(int restCount)
    {
        return Math.Min(Math.Pow(1.2, restCount), Math.Pow(restCount, 1.2)) * 1.0;
    }
}