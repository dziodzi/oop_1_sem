using System;

namespace lab1.Entities.VehicleRealisations;

public class PumpkinCarriage() : GroundVehicle("Pumpkin Carriage", 10.0, 30.0)
{
    public override double GetRestDuration(int restCount)
    {
        return Math.Min(restCount * 2.0, 15.0);
    }
}