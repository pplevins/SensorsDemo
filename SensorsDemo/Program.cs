namespace SensorsDemo;

internal class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Investigation Game – MVP ===\n");
        InvestigationManager manager = new(new FootSoldier());

        while (!manager.IsComplete)
        {
            Sensor chosen = PromptForSensor();
            manager.AttachSensor(chosen);
            manager.Activate();
        }

        Console.WriteLine("Press any key to exit …");
        Console.ReadKey();
    }

    private static Sensor PromptForSensor()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Choose a sensor to attach:");
                Console.WriteLine("1 – Audio Sensor");
                Console.WriteLine("2 – Thermal Sensor");
                Console.Write("Selection: ");
                string? input = Console.ReadLine();

                return input switch
                {
                    "1" => new BasicSensor(SensorType.Audio),
                    "2" => new BasicSensor(SensorType.Thermal),
                    _ => throw new Exception("Invalid input – try again.\n")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
