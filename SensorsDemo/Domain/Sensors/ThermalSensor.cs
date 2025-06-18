using SensorsDemo.Domain.Interfaces;

namespace SensorsDemo.Domain.Sensors;

/// <summary>
/// Class represents an audio sensor, which has the ability to reveal one correct sensor of the agent.
/// </summary>
internal sealed class ThermalSensor : Sensor
{
    /// <summary>
    /// Constructor for the thermal sensor.
    /// </summary>
    public ThermalSensor() : base(SensorType.Thermal) { }

    // <inheritdoc>
    public override void Activate(IInvestigationContext ctx)
    {
        // Reveal one correct sensor type the agent is weak to.
        var random = ctx.Agent.Weaknesses.OrderBy(_ => Guid.NewGuid()).First();
        ctx.RevealCorrectSensor(random);
    }
}
