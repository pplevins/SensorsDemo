namespace SensorsDemo;

/// <summary>
/// MVP agent – two slots, no counter‑attack, random weaknesses.
/// </summary>
internal sealed class FootSoldier : IranianAgent
{
    private static readonly Random _rng = new();

    public FootSoldier() : base(GenerateWeakness()) { }

    public override string Rank => "Foot Soldier"; // this.GetType().Name;

    private static SensorType[] GenerateWeakness()
    {
        SensorType[] pool = [SensorType.Audio, SensorType.Thermal];
        return
        [
            pool[_rng.Next(pool.Length)],
            pool[_rng.Next(pool.Length)]
        ];
    }
}
