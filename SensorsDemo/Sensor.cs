namespace SensorsDemo;

public enum SensorType
{
    Audio,
    Thermal,
    Pulse,
    Motion,
    Magnetic,
    Signal,
    Light
}

/// <summary>
/// Base class for every sensor.  Concrete sensors can override
/// behaviour such as <see cref="Activate"/>, <see cref="IsBroken"/>, etc.
/// </summary>
internal abstract class Sensor
{
    public SensorType Type { get; }

    protected Sensor(SensorType type) => Type = type;

    /// <summary>
    /// Called once per turn after the sensor is attached and the player
    /// presses “Activate”.  Override in derived sensors that have
    /// special effects (e.g. PulseSensor breaks after a few activations).
    /// </summary>
    public virtual void Activate(IranianAgent agent, InvestigationManager mgr) { /* default‑no‑op */ }

    public virtual bool IsBroken => false;
}
