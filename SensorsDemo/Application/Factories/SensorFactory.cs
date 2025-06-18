using SensorsDemo.Domain.Interfaces;
using SensorsDemo.Domain.Sensors;

namespace SensorsDemo.Application.Factories;

/// <summary>
/// Static factory class to create a sensor instances.
/// </summary>
internal static class SensorFactory
{
    /// <summary>
    /// Creating sensors according to its type.
    /// </summary>
    /// <param name="type">The sensor type to create.</param>
    /// <returns>The sensor instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">In case the given type is out of enum range.</exception>
    public static ISensor Create(SensorType type) => type switch
    {
        SensorType.Audio => new AudioSensor(),
        SensorType.Thermal => new ThermalSensor(),
        SensorType.Pulse => new PulseSensor(),
        SensorType.Motion => new MotionSensor(),
        SensorType.Magnetic => new MagneticSensor(),
        SensorType.Signal => new SignalSensor(),
        SensorType.Light => new LightSensor(),
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}
