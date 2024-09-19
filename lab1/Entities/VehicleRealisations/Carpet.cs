using System;

namespace lab1.Entities.VehicleRealisations;

class Carpet : AirVehicle
{
    public Carpet() : base("Magic Carpet", 20.0) { }

    protected override double GetSpeedRaise(double distance)
    {
        return Math.Abs(Math.Sin(distance)) / 5;
    }
}