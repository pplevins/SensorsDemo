namespace SensorsDemo.Domain.Agents;

/// <summary>
/// Class to represent agent with the rank of squad leader,
/// which has the ability to removes 1 random sensor every 3 turns.
/// </summary>
internal sealed class SquadLeader : IranianAgent
{
    /// <summary>
    /// Constructor for the squad leader.
    /// </summary>
    public SquadLeader() : base("Squad Leader", 4) { }

    // <inheritdoc>
    protected override CounterAttackPlan BuildPlan() => Plan(3, 1);
}
