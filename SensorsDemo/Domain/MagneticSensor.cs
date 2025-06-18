namespace SensorsDemo.Domain;

/// <summary>
/// Class to represent magnetic sensor,
/// which has the ability to cancels agent's counter attack twice when matched correctly.
/// </summary>
internal sealed class MagneticSensor : Sensor
{
    private int _cancelsLeft = 2;

    /// <summary>
    /// Constructor for the magnetic sensor.
    /// </summary>
    public MagneticSensor() : base(SensorType.Magnetic) { }

    // <inheritdoc>
    public override void Activate(IInvestigationContext ctx)
    {
        if (_cancelsLeft > 0 && ctx.Agent.Matches(Type))
        {
            ctx.CancelNextCounterAttack();
            _cancelsLeft--;
        }
    }
}
