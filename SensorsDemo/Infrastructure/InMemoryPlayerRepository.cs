namespace SensorsDemo.Infrastructure;

/// <summary>
/// Class to give in-memory infrastructure to save historical data for players in the game.
/// </summary>
internal sealed class InMemoryPlayerRepository : IPlayerRepository
{
    private readonly Dictionary<string, string> _db = [];

    // <inheritdoc>
    public void SaveHighestRank(string playerId, string rank) => _db[playerId] = rank;
    // <inheritdoc>
    public string? GetHighestRank(string playerId) => _db.TryGetValue(playerId, out var r) ? r : null;
}
