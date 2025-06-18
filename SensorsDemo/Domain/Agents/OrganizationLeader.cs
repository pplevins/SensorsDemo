namespace SensorsDemo.Domain.Agents;

/// <summary>
/// Class to represent the higher rank of iranian agent,
/// which has the ability to:
/// <list type="bullet">
/// <item>remove 1 sensor every 3 turns.</item>
/// <item>reset weakness list and removes all attached sensors every 10 turns.</item>
/// </list>
/// </summary>
internal sealed class OrganizationLeader : IranianAgent
{
    /// <summary>
    /// Constructor for the organization leader.
    /// </summary>
    public OrganizationLeader() : base("Organization Leader", 8) { }

    // <inheritdic>
    protected override CounterAttackPlan BuildPlan() => Plan(3, 1, majorReset: true);
}
