namespace SensorsDemo.Domain.Interfaces;

/// <summary>
/// Enum that represents types of sensors in the game
/// </summary>
public enum SensorType
{
    Audio,
    Thermal,
    Pulse,
    Motion,
    Magnetic,
    Signal,
    Light
}


/// <summary>
/// Sensor interface represent sensor functionality.
/// </summary>
internal interface ISensor
{
    /// <summary>
    /// Types of the sensors as <see cref="SensorType"/>
    /// </summary>
    SensorType Type { get; }
    
    /// <summary>
    /// Checks if the sensor is broken.
    /// </summary>
    bool IsBroken { get; }
    
    /// <summary>
    /// Runs once per turn AFTER it has been attached.
    /// </summary>
    /// <param name="ctx">The context allows sensors to interact with the investigation (e.g., reveal info).</param>
    void Activate(IInvestigationContext ctx);
}
