namespace SensorsDemo.Domain;

/// <summary>
/// Class represents a pulse sensor, which breaks after 3 activations.
/// </summary>
internal sealed class PulseSensor : LimitedUseSensor
{
    /// <summary>
    /// Constructor for the pulse sensor.
    /// </summary>
    public PulseSensor() : base(SensorType.Pulse, 3) { }

    // <inhertidoc>
    public override void Activate(IInvestigationContext ctx) => ConsumeUse();
}
