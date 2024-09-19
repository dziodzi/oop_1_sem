using System;

namespace lab1.Entities.VehicleRealisations;

public class BootsOfSpeed() : GroundVehicle("Boots of Speed", 50.0, 5.0)
{
    public override double GetRestDuration(int restCount)
    {
        double randomModificator = new Random().NextDouble();
        return randomModificator * restCount;
    }
}