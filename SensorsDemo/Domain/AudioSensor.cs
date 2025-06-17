namespace SensorsDemo.Domain;

/// <summary>
/// Class represents an audio sensor, the most basic sensor with no special abilities.
/// </summary>
internal sealed class AudioSensor : Sensor
{
    /// <summary>
    /// Constructor for the audio sensor.
    /// </summary>
    public AudioSensor() : base(SensorType.Audio) { }

    // <inheritdoc>
    public override void Activate(IInvestigationContext ctx) { /* No‑op */ }
}
