namespace SensorsDemo.Infrastructure;

/// <summary>
/// Interface to represent repository infrastructure for the game,
/// that gives as the option to save historical data over time in the game.
/// </summary>
internal interface IPlayerRepository
{
    /// <summary>
    /// Saving the higher rank of an agent that got exposed in the game.
    /// </summary>
    /// <param name="playerId">ID of the player.</param>
    /// <param name="rank">Rank of the higher agent.</param>
    void SaveHighestRank(string playerId, string rank);

    /// <summary>
    /// Gets the highest rank of an agent that got exposed in the game by player ID
    /// </summary>
    /// <param name="playerId">ID of the player.</param>
    /// <returns>Rank of the higher agent.</returns>
    string? GetHighestRank(string playerId);
}
