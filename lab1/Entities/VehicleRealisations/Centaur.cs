using System;

namespace lab1.Entities.VehicleRealisations;

class Centaur : GroundVehicle
{
    public Centaur() : base("Centaur", 35.0, 75.0) { }

    public override double GetRestDuration(int restCount)
    {
        return Math.Min(Math.Pow(1.2, restCount), Math.Pow(restCount, 1.2)) * 1.0;
    }
}