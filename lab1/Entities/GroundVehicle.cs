namespace lab1.Entities;

public abstract class GroundVehicle(string name, double speed, double restInterval) : Vehicle(name, speed)
{
    private double RestInterval { get; } = restInterval;

    public abstract double GetRestDuration(int restCount);

    public override double CalculateRaceTime(double distance)
    {
        double timeMoving = distance / Speed;
        int restCount = (int)(timeMoving / RestInterval);
        double totalRestTime = 0;

        for (int i = 1; i <= restCount; i++)
        {
            totalRestTime += GetRestDuration(i);
        }

        return timeMoving + totalRestTime;
    }
}