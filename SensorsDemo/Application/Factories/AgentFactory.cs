using SensorsDemo.Domain.Agents;

namespace SensorsDemo.Application.Factories;

/// <summary>
/// Static factory class to create a agent instances.
/// </summary>
internal static class AgentFactory
{
    /// <summary>
    /// Creating agent instance according to the given level.
    /// </summary>
    /// <param name="level">The agent's level.</param>
    /// <returns>The agent instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">In case the level number is out of agent's level range.</exception>
    public static IranianAgent Create(int level) => level switch
    {
        1 => new FootSoldier(),
        2 => new SquadLeader(),
        3 => new SeniorCommander(),
        4 => new OrganizationLeader(),
        _ => throw new ArgumentOutOfRangeException(nameof(level))
    };
}
