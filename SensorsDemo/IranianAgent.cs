namespace SensorsDemo;

/// <summary>
/// Abstract Iranian agent. Holds the secret weakness list and exposes
/// primitives that game logic calls each turn.
/// </summary>
internal abstract class IranianAgent
{
    private readonly SensorType[] _weaknesses;   // e.g. [Thermal, Thermal]
    private int _turnsElapsed;

    protected IranianAgent(SensorType[] weaknesses) => _weaknesses = weaknesses;

    public int SlotCount => _weaknesses.Length;
    public abstract string Rank { get; }

    /// <summary>
    /// Returns how many attached sensors match this agent’s secret list.
    /// </summary>
    public int EvaluateMatches(IEnumerable<Sensor> attached)
    {
        var remaining = _weaknesses.ToList();
        int matched = 0;

        foreach (var s in attached)
        {
            int idx = remaining.IndexOf(s.Type);
            if (idx >= 0)
            {
                remaining.RemoveAt(idx);
                matched++;
            }
        }
        return matched;
    }

    public bool IsExposed(IEnumerable<Sensor> attached) => EvaluateMatches(attached) == SlotCount;

    /// <summary>
    /// Override in derived ranks that perform a counter‑attack.
    /// </summary>
    public virtual void CounterAttack(List<Sensor> attached) { /* Foot Soldier has none */ }

    public virtual bool ShouldCounterAttack => false;

    public void AdvanceTurn() => _turnsElapsed++;
}
