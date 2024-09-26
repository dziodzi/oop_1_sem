namespace lab1.Entities;

internal abstract class AirVehicle(string name, double speed) : Vehicle(name, speed)
{
    protected abstract double GetSpeedRaise(double distance);

    public override double CalculateRaceTime(double distance)
    {
        var reducedSpeed = Speed * (1 + GetSpeedRaise(distance));
        return distance / reducedSpeed;
    }
}