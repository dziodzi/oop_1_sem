namespace lab1.Entities;

abstract class AirVehicle(string name, double speed) : Vehicle(name, speed)
{
    protected abstract double GetSpeedRaise(double distance);

    public override double CalculateRaceTime(double distance)
    {
        double reducedSpeed = Speed * (1 + GetSpeedRaise(distance));
        return distance / reducedSpeed;
    }
}