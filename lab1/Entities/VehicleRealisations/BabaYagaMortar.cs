using System;

namespace lab1.Entities.VehicleRealisations;

class BabaYagaMortar() : AirVehicle("Baba Yaga's Mortar", 15.0)
{
    protected override double GetSpeedRaise(double distance)
    {
        double randomModificator = new Random().NextDouble();
        if (randomModificator > 0.5) return Math.Pow(0.99, distance) - randomModificator;
        return randomModificator - Math.Pow(0.99, distance);
    }
}