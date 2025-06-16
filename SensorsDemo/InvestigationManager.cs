namespace SensorsDemo;

/// <summary>
/// Central coordinator that owns the agent + the list of currently attached sensors.
/// Keeps track of whose turn it is and prints feedback to the console UI.
/// </summary>
internal sealed class InvestigationManager
{
    private readonly IranianAgent _agent;
    private readonly List<Sensor> _attached = [];
    private int _turnNo;

    public InvestigationManager(IranianAgent agent) => _agent = agent;

    public void AttachSensor(Sensor sensor) => _attached.Add(sensor);

    public void Activate()
    {
        _turnNo++;
        _agent.AdvanceTurn();

        // 1. Sensors perform their own activation hooks (important for later stages).
        foreach (Sensor s in _attached.ToList()) // ToList so we can modify collection
        {
            if (!s.IsBroken)
                s.Activate(_agent, this);
        }

        // 2. Evaluate exposure.
        int matched = _agent.EvaluateMatches(_attached);
        Console.WriteLine($"Turn {_turnNo}: {matched}/{_agent.SlotCount}");

        // 3. Counter‑attack phase (future expansion – noop for Foot Soldier).
        if (_agent.ShouldCounterAttack)
            _agent.CounterAttack(_attached);

        // 4. Exposure check.
        if (_agent.IsExposed(_attached))
            Console.WriteLine("\nAgent exposed – mission accomplished!\n");
    }

    public bool IsComplete => _agent.IsExposed(_attached);
}
