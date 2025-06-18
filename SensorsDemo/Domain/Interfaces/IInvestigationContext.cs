namespace SensorsDemo.Domain.Interfaces;

/// <summary>
/// Context exposed to sensors during <see cref="ISensor.Activate(IInvestigationContext)"/> [Domain boundary].
/// </summary>
internal interface IInvestigationContext
{
    /// <summary>
    /// Agent property in the investigation context.
    /// </summary>
    IranianAgent Agent { get; }

    /// <summary>
    /// A read-only collection of attached sensor
    /// </summary>
    IReadOnlyCollection<ISensor> AttachedSensors { get; }

    /// <summary>
    /// Reveal correct sensor from the agent secret weaknesses.
    /// </summary>
    /// <param name="type">type of </param>
    void RevealCorrectSensor(SensorType type);

    /// <summary>
    /// Reveal some secret info about the agent
    /// </summary>
    /// <param name="infoKey">the key of the info property.</param>
    /// <param name="value">the value to reveal.</param>
    void RevealAgentInfo(string infoKey, string value);

    /// <summary>
    /// Cancles the next agent's counter attack.
    /// </summary>
    void CancelNextCounterAttack();
}