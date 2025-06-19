using MySql.Data.MySqlClient;

namespace SensorsDemo.Infrastructure;

/// <summary>
/// Class to give MySql infrastructure to save historical data for players in the game.
/// </summary>
internal class MySqlPlayerRepository : IPlayerRepository
{
    private readonly string _connectionString;

    /// <summary>
    /// Constructor for the MySql infra class.
    /// </summary>
    /// <param name="connectionString">The connection path to the database.</param>
    public MySqlPlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // <inhertidoc>
    public void SaveHighestRank(string playerId, string rank)
    {
        using MySqlConnection conn = new(_connectionString);
        conn.Open();
        string sql = @"
                INSERT INTO PlayerRanks (PlayerId, HighestRank)
                VALUES (@PlayerId, @Rank)
                ON DUPLICATE KEY UPDATE HighestRank = @Rank;
            ";
        using MySqlCommand cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PlayerId", playerId);
        cmd.Parameters.AddWithValue("@Rank", rank);
        cmd.ExecuteNonQuery();
    }

    // <inhertidoc>
    public string? GetHighestRank(string playerId)
    {
        using MySqlConnection conn = new(_connectionString);
        conn.Open();
        string sql = "SELECT HighestRank FROM PlayerRanks WHERE PlayerId = @PlayerId";
        using MySqlCommand cmd = new(sql, conn);
        cmd.Parameters.AddWithValue("@PlayerId", playerId);
        using MySqlDataReader reader = cmd.ExecuteReader();
        return reader.Read() ? 
            reader.GetString("HighestRank") 
            : null;
    }
}
