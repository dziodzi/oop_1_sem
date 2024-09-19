﻿namespace lab1.Entities;

public abstract class Vehicle(string name, double speed)
{
    public string Name { get; } = name;
    public double Speed { get; } = speed;
    
    public abstract double CalculateRaceTime(double distance);
}