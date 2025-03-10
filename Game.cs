using System;
using System.Collections.Generic;

class Game
{
    private Player player;
    private List<Room> rooms;
    private Random random = new Random();

    public Game()
    {
        player = new Player("Robber (YOU)", 100);
        rooms = Room.InitializeRooms();
    }

    public void Start()
    {
        Console.WriteLine("Welcome to the heist! Go through each room to the end to grab the money for yourself!");
        int currentRoomIndex = 0;

        while (currentRoomIndex < rooms.Count && player.Health > 0)
        {
            Console.WriteLine("\n--- Choose an Action ---");
            Console.WriteLine("1. Check Stats");
            Console.WriteLine("2. Proceed to the next room");
            Console.Write("Enter your choice (or press Enter to proceed to the next room anyway): ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                player.DisplayStats();
                continue; // Stay in menu
            }

            Room currentRoom = rooms[currentRoomIndex];
            Console.WriteLine("\n" + currentRoom.GetDescription());

            if (currentRoom.HasEnemy)
            {
                Enemy enemy = currentRoom.SpawnEnemy();
                Console.WriteLine($"An enemy appears in front of your way: {enemy.Name}!");
                Battle(enemy);
                if (player.Health <= 0)
                {
                    Console.WriteLine("Game Over.");
                    return;
                }
            }

            if (currentRoom.HasKey)
            {
                Console.WriteLine("You found a mysterious key!");
                player.PickUpItem("Key");
            }

            if (currentRoom.HasHealingItem)
            {
                int healAmount = new Random().Next(5, 25);
                Console.WriteLine("You found a health potion! Adding health...");
                player.Heal(healAmount);
            }

            Console.WriteLine("Press Enter to move to the next room...");
            Console.ReadLine();
            currentRoomIndex++;
        }

        Console.WriteLine(player.Health > 0 ? "Congratulations! You reached the vault and got rich!" : "Game Over.");
    }

    private void Battle(Enemy enemy)
    {
        while (player.Health > 0 && enemy.Health > 0)
        {
            Console.WriteLine("\n--- Encounter Options ---");
            Console.WriteLine("1. Lower Attack (More Accurate, Less Damage dealt)");
            Console.WriteLine("2. Upper Attack (More Damage dealt, Less Accurate)");
            Console.WriteLine($"Your Health: {player.Health} // Enemy's Health: {enemy.Health}");
            Console.Write("Choose an attack: ");
            string attackChoice = Console.ReadLine();

            if (attackChoice == "1" || attackChoice == "2")
            {
                int playerDamage = attackChoice == "1" ? player.LowerAttack() : player.UpperAttack();
                enemy.TakeDamage(playerDamage);
                Console.WriteLine($"You dealt {playerDamage} damage to {enemy.Name}!");
            }
            else
            {
                Console.WriteLine("Invalid input. You flinched, and the enemy will strike you in return.");
            }

            if (enemy.Health <= 0) break;

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            int enemyDamage = enemy.Attack();
            player.TakeDamage(enemyDamage);
            Console.WriteLine($"{enemy.Name} dealt {enemyDamage} damage to you!");

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        if (player.Health <= 0)
        {
            Console.WriteLine("Game Over.");
        }
    }
}
