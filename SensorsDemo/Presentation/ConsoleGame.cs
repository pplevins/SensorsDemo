using SensorsDemo.Application;
using SensorsDemo.Application.Factories;
using SensorsDemo.Domain.Agents;
using SensorsDemo.Domain.Interfaces;
using SensorsDemo.Infrastructure;

namespace SensorsDemo.Presentation;

/// <summary>
/// A manager class that runs the game in a console.
/// </summary>
internal sealed class ConsoleGame
{
    private readonly IPlayerRepository _repo;
    private readonly string _playerId = "guest";

    /// <summary>
    /// Constructor for the console game class
    /// </summary>
    /// <param name="repo">The infra class to store game data.</param>
    public ConsoleGame(IPlayerRepository repo) => _repo = repo;

    /// <summary>
    /// Running the game.
    /// </summary>
    public void Run()
    {
        Console.WriteLine("=== Iranian Agent Investigation – v2 ===\n");
        for (int level = 1; level <= 4; level++)
        {
            // Choosing an agent to exposed
            IranianAgent agent = AgentFactory.Create(level);
            Console.WriteLine($"Level {level}: {agent.Rank} – expose {agent.Weaknesses.Length} weaknesses\n");
            InvestigationEngine engine = new(agent);

            while (true)
            {
                // Attaching sensor
                ShowStatus(engine);
                ISensor sensor = PromptSensor();
                engine.AttachSensor(sensor);

                // Getting the result, info messages, and checking for counter-attacks.
                InvestigationTurnResult result = engine.NextTurn();
                foreach (string msg in result.InfoMessages) 
                    Console.WriteLine($"\t- {msg}");
                if (result.CounterAttackOccurred) 
                    Console.WriteLine("\t!!! Counter‑attack! Some sensors were removed.");

                // Checking if the agent exposed.
                if (result.AgentExposed)
                {
                    Console.WriteLine($"Yayyy! :-) Agent exposed in {result.Turn} turns!\n");
                    _repo.SaveHighestRank(_playerId, agent.Rank);
                    break;
                }
            }
        }

        Console.WriteLine("Congratulations – all agents defeated!");
    }

    /// <summary>
    /// Showing the status of the game to the user.
    /// </summary>
    /// <param name="engine">The investigation engine.</param>
    private static void ShowStatus(InvestigationEngine engine)
    {
        int matched = engine.Agent.AttachedMatchCount(engine.AttachedSensors);
        Console.WriteLine($"Turn {engine.TurnNo + 1} – {matched}/{engine.Agent.Weaknesses.Length} correct sensors attached");
        Console.WriteLine("Attached sensors: " + string.Join(", ", engine.AttachedSensors.Select(s => s.Type)));
    }

    /// <summary>
    /// Asking the user for a sensor.
    /// </summary>
    /// <returns>Sensor instance choosed by the user.</returns>
    private static ISensor PromptSensor()
    {
        while (true)
        {
            Console.WriteLine("Choose a sensor to attach:");
            int idx = 1;
            foreach (SensorType t in Enum.GetValues<SensorType>())
                Console.WriteLine($"{idx++}. {t}");
            Console.Write("Selection: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= Enum.GetValues<SensorType>().Length)
            {
                return SensorFactory.Create((SensorType)(choice - 1));
            }
            Console.WriteLine("Invalid – try again.\n");
        }
    }
}
