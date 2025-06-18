using SensorsDemo.Domain.Interfaces;

namespace SensorsDemo.Domain.Sensors;

/// <summary>
/// Class represents a motion sensor, which breaks after 3 activations.
/// </summary>
internal sealed class MotionSensor : LimitedUseSensor
{
    /// <summary>
    /// Constructor for the pulse sensor.
    /// </summary>
    public MotionSensor() : base(SensorType.Motion, 3) { }

    // <inhertidoc>
    public override void Activate(IInvestigationContext ctx) => ConsumeUse();
}
