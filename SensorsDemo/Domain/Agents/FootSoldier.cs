namespace SensorsDemo.Domain.Agents;

/// <summary>
/// Class to represent foot soldier,
/// the most basic ranked iranian agent with no special counter attack abilities.
/// </summary>
internal sealed class FootSoldier : IranianAgent
{
    /// <summary>
    /// Constructor for the foot soldier.
    /// </summary>
    public FootSoldier() : base("Foot Soldier", 2) { }

    // <inheritdoc>
    protected override CounterAttackPlan BuildPlan() => default; // None
}
