namespace SensorsDemo;

/// <summary>
/// Plain sensor with no special ability – good enough for MVP.
/// </summary>
internal sealed class BasicSensor : Sensor
{
    public BasicSensor(SensorType type) : base(type) { }
}