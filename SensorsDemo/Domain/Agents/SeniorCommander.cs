namespace SensorsDemo.Domain.Agents;

/// <summary>
/// Class to represant iranian agent with the rank of senior commander,
/// which has the ability to remove 2 random sensors every 3 turns.
/// </summary>
internal sealed class SeniorCommander : IranianAgent
{
    /// <summary>
    /// Constructor for the senior commander.
    /// </summary>
    public SeniorCommander() : base("Senior Commander", 6) { }

    // <inheritdoc>
    protected override CounterAttackPlan BuildPlan() => Plan(3, 2);
}
