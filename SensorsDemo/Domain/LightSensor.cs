namespace SensorsDemo.Domain;

/// <summary>
/// Class represent light sensor,
/// which has the ability to reveal 2 agent's fields of information.
/// </summary>
internal class LightSensor : Sensor
{
    /// <summary>
    /// Constructor for the light sensor.
    /// </summary>
    public LightSensor() : base(SensorType.Light) { }
    public override void Activate(IInvestigationContext ctx)
    {
        foreach (var kv in ctx.Agent.PublicInfo.OrderBy(_ => Guid.NewGuid()).Take(2))
            ctx.RevealAgentInfo(kv.Key, kv.Value);
    }
}
