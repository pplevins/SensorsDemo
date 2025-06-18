using SensorsDemo.Domain.Interfaces;

namespace SensorsDemo.Domain.Agents;

/// <summary>
/// Struct object to represent agent's counter attack plan.
/// </summary>
public struct CounterAttackPlan
{
    /// <summary>
    /// number of sensors agent can remove.
    /// </summary>
    public int SensorsToRemove;
    /// <summary>
    /// <see cref="true"/> if agent can remove all sensors, <see cref="false"/> otherwise.
    /// </summary>
    public bool RemoveAllAttached;
    /// <summary>
    /// <see cref="true"/> if the agent can reset all of his weaknesses, <see cref="false"/> otherwise.
    /// </summary>
    public bool ResetWeaknesses;
}

/// <summary>
/// A base class for all iranian agents in the game.
/// </summary>
internal abstract class IranianAgent
{
    protected readonly Random _rng = new();
    private int _turns = 0;
    private int _sinceMajorCounter = 0;

    /// <summary>
    /// Rank of the agent
    /// </summary>
    public string Rank { get; }

    /// <summary>
    /// sensors types that are the weaknesses of the agent.
    /// </summary>
    public SensorType[] Weaknesses { get; }

    /// <summary>
    /// Public info about the agent that can be revealed.
    /// </summary>
    public Dictionary<string, string> PublicInfo { get; } = [];

    /// <summary>
    /// Constructor for iranian agent.
    /// </summary>
    /// <param name="rank">Rank of the agent.</param>
    /// <param name="slots">number of sensors slot.</param>
    protected IranianAgent(string rank, int slots)
    {
        Rank = rank;
        Weaknesses = Enumerable.Range(0, slots)
                               .Select(_ => (SensorType)_rng.Next(Enum.GetNames(typeof(SensorType)).Length))
                               .ToArray();
        PublicInfo["Rank"] = rank;
    }

    /// <summary>
    /// Checks if a given type is matched to the agent's weaknesses.
    /// </summary>
    /// <param name="type">Sensor's type to match.</param>
    /// <returns><see cref="true"/> if the sensor is match, <see cref="false"/> otherwise.</returns>
    public bool Matches(SensorType type) => Weaknesses.Contains(type); // TODO: Check correctness.
    
    /// <summary>
    /// Check if all attached sensors are correct and the agent's exposed.
    /// </summary>
    /// <param name="attached">Attached sensors collection.</param>
    /// <returns><see cref="true"/> if the agent is exposed, <see cref="false"/> otherwise.</returns>
    public bool IsExposed(IEnumerable<ISensor> attached) => AttachedMatchCount(attached) == Weaknesses.Length;

    /// <summary>
    /// Counts number of matched sensors with the weaknesses list.
    /// </summary>
    /// <param name="attached">Attached sensors collection.</param>
    /// <returns>number of matched sensors.</returns>
    public int AttachedMatchCount(IEnumerable<ISensor> attached)
    {
        var rem = Weaknesses.ToList();
        int match = 0;
        foreach (var s in attached)
        {
            int idx = rem.IndexOf(s.Type);
            if (idx != -1) { match++; rem.RemoveAt(idx); }
        }
        return match;
    }

    // ---------- Counter‑attack template  ----------------------------------

    /// <summary>
    /// Advancing the turns to make the counter attack plan.
    /// </summary>
    /// <returns>Counter attack plan in struct.</returns>
    public CounterAttackPlan NextTurnPlan()
    {
        _turns++;
        _sinceMajorCounter++;
        return BuildPlan();
    }

    /// <summary>
    /// Building the counter attack plan.
    /// </summary>
    /// <returns>The counter attack for the agent.</returns>
    protected abstract CounterAttackPlan BuildPlan();

    /// <summary>
    /// Creating the plan according to the parameters.
    /// </summary>
    /// <param name="everyNTurns">Every how many turns can the counter attack happen.</param>
    /// <param name="remove">How many sensors can be removed.</param>
    /// <param name="majorReset"><see cref="true"/> if the agent can reset all the sensors, <see cref="false"/> otherwise.</param>
    /// <returns></returns>
    protected CounterAttackPlan Plan(int everyNTurns, int remove, bool majorReset = false)
    {
        CounterAttackPlan plan = new();

        if (everyNTurns > 0 && _turns % everyNTurns == 0)
        {
            plan.SensorsToRemove = remove;
        }

        if (majorReset && _sinceMajorCounter >= 10)
        {
            plan.ResetWeaknesses = true;
            plan.RemoveAllAttached = true;
            _sinceMajorCounter = 0;
        }

        return plan;
    }
}
