using SensorsDemo.Application.Factories;
using SensorsDemo.Domain.Agents;
using SensorsDemo.Domain.Interfaces;

namespace SensorsDemo.Application;

/// <summary>
/// A struct represent combined actions happened on one turn and their result.
/// </summary>
public struct InvestigationTurnResult
{
    /// <summary>
    /// Trun number
    /// </summary>
    public int Turn;
    /// <summary>
    /// Number of matched sensors.
    /// </summary>
    public int Matched;
    /// <summary>
    /// Number of slots in agent's weaknesses.
    /// </summary>
    public int Slots;
    /// <summary>
    /// <see cref="true"/> if agent is exposed, <see cref="false"/> otherwise.
    /// </summary>
    public bool AgentExposed;
    /// <summary>
    /// Array of info messages to show to the player.
    /// </summary>
    public string[] InfoMessages;
    /// <summary>
    /// <see cref="true"/> if counter attack occurred, <see cref="false"/> otherwise.
    /// </summary>
    public bool CounterAttackOccurred;
}

/// <summary>
/// An engine class to run the investigation on the agent.
/// Used when attaching sensor and activating in <see cref="ISensor.Activate(IInvestigationContext)"/>.
/// </summary>
internal sealed class InvestigationEngine : IInvestigationContext
{
    private readonly List<ISensor> _attached = [];
    private readonly Queue<string> _infoReveals = new();
    private readonly Random _rng = new();
    private bool _counterAttackCancelled;

    /// <summary>
    /// Agent to investigate.
    /// </summary>
    public IranianAgent Agent { get; private set; }

    /// <summary>
    /// Attached sensors by the user.
    /// </summary>
    public IReadOnlyCollection<ISensor> AttachedSensors => _attached.AsReadOnly();

    /// <summary>
    /// Turn counter.
    /// </summary>
    public int TurnNo { get; private set; }

    /// <summary>
    /// Constructor to the investigation engine.
    /// </summary>
    /// <param name="agent">Agent to investigate.</param>
    public InvestigationEngine(IranianAgent agent) => Agent = agent;

    // ---------------- API used by Presentation ---------------------------

    /// <summary>
    /// Attaching one sensor to the agent.
    /// </summary>
    /// <param name="sensor">Sensor to attach.</param>
    public void AttachSensor(ISensor sensor) => _attached.Add(sensor);

    /// <summary>
    /// Advincing the turn, and operating the turn logic.
    /// </summary>
    /// <returns></returns>
    public InvestigationTurnResult NextTurn()
    {
        TurnNo++;
        _counterAttackCancelled = false;
        _infoReveals.Clear();

        // 1. Sensor activation phase
        foreach (var s in _attached.Where(s => !s.IsBroken).ToArray())
            s.Activate(this);

        // 2. Counter‑attack phase
        CounterAttackPlan plan = Agent.NextTurnPlan();
        if (!_counterAttackCancelled)
        {
            if (plan.RemoveAllAttached)
                _attached.Clear();
            else if (plan.SensorsToRemove > 0)
            {
                for (int i = 0; i < plan.SensorsToRemove && _attached.Count != 0; i++)
                {
                    int idx = _rng.Next(_attached.Count);
                    _attached.RemoveAt(idx);
                }
            }
        }

        // 3. Reset weaknesses if ordered
        if (plan.ResetWeaknesses)
        {
            Agent = AgentFactory.Create(4); // Re‑spawn as Org Leader with fresh weaknesses (simple choice)
        }

        // 4. Exposure evaluation
        int match = Agent.AttachedMatchCount(_attached);
        bool exposed = Agent.IsExposed(_attached);

        return new InvestigationTurnResult
        {
            Turn = TurnNo,
            Matched = match,
            Slots = Agent.Weaknesses.Length,
            AgentExposed = exposed,
            InfoMessages = _infoReveals.ToArray(),
            CounterAttackOccurred = !_counterAttackCancelled && (plan.SensorsToRemove > 0 || plan.RemoveAllAttached)
        };
    }

    // ------------- IInvestigationContext implementation ------------------

    // <inheritdoc>
    void IInvestigationContext.RevealCorrectSensor(SensorType type)
        => _infoReveals.Enqueue($"Reveal: one weakness is {type}");

    // <inhertidoc>
    void IInvestigationContext.RevealAgentInfo(string infoKey, string value)
        => _infoReveals.Enqueue($"Info: {infoKey} = {value}");

    // <inheritdoc>
    void IInvestigationContext.CancelNextCounterAttack()
        => _counterAttackCancelled = true;
}
