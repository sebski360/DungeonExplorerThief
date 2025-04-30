using System;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
