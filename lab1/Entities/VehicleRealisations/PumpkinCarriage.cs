using System;

namespace lab1.Entities.VehicleRealisations;

public class PumpkinCarriage() : GroundVehicle("Pumpkin Carriage", 10.0, 35.0)
{
    public override double GetRestDuration(int restCount)
    {
        return Math.Min(restCount * 1.0, 7.0);
    }
}