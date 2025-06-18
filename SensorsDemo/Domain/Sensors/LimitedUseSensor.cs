using SensorsDemo.Domain.Interfaces;

namespace SensorsDemo.Domain.Sensors;

/// <summary>
/// An abstract class to represent sensor instance with limited use operation.
/// </summary>
internal abstract class LimitedUseSensor : Sensor
{
    private int _usesLeft;

    /// <summary>
    /// Constructor for the LimitedUseSensor class.
    /// </summary>
    /// <param name="type">type of the actual sensor.</param>
    /// <param name="uses">number of uses left.</param>
    protected LimitedUseSensor(SensorType type, int uses) : base(type) => _usesLeft = uses;

    // <inheritdoc>
    public override bool IsBroken => _usesLeft <= 0;

    /// <summary>
    /// Counting the uses in the sensor.
    /// </summary>
    /// <returns><see cref="true"/> if any use left, <see cref="false"/> otherwise.</returns>
    protected bool ConsumeUse()
    {
        if (_usesLeft <= 0) 
            return false;
        _usesLeft--;
        return true;
    }
}
