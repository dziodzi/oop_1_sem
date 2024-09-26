namespace lab1.Entities.VehicleRealisations;

internal class Carpet() : AirVehicle ("Magic Carpet", 20.0)
{
    protected override double GetSpeedRaise(double distance)
    {
        return Math.Abs(Math.Sin(distance)) / 5;
    }
}