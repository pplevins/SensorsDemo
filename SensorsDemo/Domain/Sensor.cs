namespace SensorsDemo.Domain;

/// <summary>
/// A base class to represnt sensor instance
/// </summary>
internal abstract class Sensor : ISensor
{
    // <inheritdoc>
    public SensorType Type { get; }
    
    // <inheritdoc>
    public virtual bool IsBroken => false;

    /// <summary>
    /// A base constructor for the sensor instance.
    /// </summary>
    /// <param name="type">Type of the sensor</param>
    protected Sensor(SensorType type) => Type = type;
    
    // <inheritdoc>
    public abstract void Activate(IInvestigationContext ctx);
}
