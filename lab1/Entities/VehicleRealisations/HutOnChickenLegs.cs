namespace lab1.Entities.VehicleRealisations;

internal class HutOnChickenLegs() : GroundVehicle("Hut on Chicken Legs", 5.0, 50.0)
{
    protected override double GetRestDuration(int restCount)
    {
        return restCount % 3 != 0 ? 0.0 : 15.0;
    }
}