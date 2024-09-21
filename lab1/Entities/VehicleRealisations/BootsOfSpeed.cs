namespace lab1.Entities.VehicleRealisations;

internal class BootsOfSpeed() : GroundVehicle("Boots of Speed", 50.0, 5.0)
{
    protected override double GetRestDuration(int restCount)
    {
        var randomModificator = new Random().NextDouble();
        return randomModificator * restCount;
    }
}