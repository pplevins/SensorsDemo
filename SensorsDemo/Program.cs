using SensorsDemo.Infrastructure;
using SensorsDemo.Presentation;

namespace SensorsDemo;

internal class Program
{
    public static void Main()
    {
        InMemoryPlayerRepository repo = new();
        ConsoleGame game = new(repo);
        game.Run();
    }
}
