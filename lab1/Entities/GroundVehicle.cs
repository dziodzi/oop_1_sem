namespace lab1.Entities;

internal abstract class GroundVehicle(string name, double speed, double restInterval) : Vehicle(name, speed)
{
    protected abstract double GetRestDuration(int restCount);

    public override double CalculateRaceTime(double distance)
    {
        var timeMoving = distance / Speed;
        var restCount = (int)(timeMoving / restInterval);
        double totalRestTime = 0;

        for (var i = 1; i <= restCount; i++)
        {
            totalRestTime += GetRestDuration(i);
        }

        return timeMoving + totalRestTime;
    }
}