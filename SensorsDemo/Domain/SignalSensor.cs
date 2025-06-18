namespace SensorsDemo.Domain;

/// <summary>
/// Class represents signal sensor,
/// which has the ability to reveal one field of information about the terrorist.
/// </summary>
internal sealed class SignalSensor : Sensor
{
    /// <summary>
    /// Constructor for the signal sensor.
    /// </summary>
    public SignalSensor() : base(SensorType.Signal) { }

    // <inheritdoc>
    public override void Activate(IInvestigationContext ctx)
    {
        var kv = ctx.Agent.PublicInfo.OrderBy(_ => Guid.NewGuid()).First();
        ctx.RevealAgentInfo(kv.Key, kv.Value);
    }
}
