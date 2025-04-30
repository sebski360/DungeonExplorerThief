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
        // Prompt for starting weapon
        Console.WriteLine("Choose your starting weapon!");
        Console.WriteLine("1. Knife");
        Console.WriteLine("2. Longblade");
        Console.Write("Enter your choice: ");
        var weaponChoice = Console.ReadLine();
        if (weaponChoice == "1")
            player.PickUpItem(new Knife());
            // player.PickUpItem(new Longblade()); 
            // Add this to test weapon damage sorting
        else
            player.PickUpItem(new Longblade());

        Console.WriteLine("Welcome to the heist! Go through each room to the end to grab the money for yourself!");
        int currentRoomIndex = 0;

        while (currentRoomIndex < rooms.Count && player.Health > 0)
        {
            Console.WriteLine("\n--- Choose an Action ---");
            Console.WriteLine("1. Check Stats");
            Console.WriteLine("2. Check Inventory");
            Console.WriteLine("3. Proceed to the next room");
            Console.Write("Enter your choice (or press Enter to proceed anyway): ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                player.DisplayStats();
                continue;
            }
            if (choice == "2")
            {
                player.ShowInventory();
                continue;
            }

            var room = rooms[currentRoomIndex];
            Console.WriteLine("\n" + room.GetDescription());

            if (room.HasEnemy)
            {
                var enemy = room.SpawnEnemy();
                Console.WriteLine($"An enemy appears before you: {enemy.Name}!");
                Battle(enemy);
                if (player.Health <= 0)
                {
                    Console.WriteLine("Game Over.");
                    return;
                }
            }

            if (room.HasKey)
                player.PickUpItem(new Key());

            if (room.HasHealingItem)
            {
                int healAmt = random.Next(5, 25);
                Console.WriteLine("You found a health potion! Restoring health...");
                player.Heal(healAmt);
            }

            Console.WriteLine("Press Enter to move to the next room...");
            Console.ReadLine();
            currentRoomIndex++;
        }

        Console.WriteLine(player.Health > 0
            ? "Congratulations! You reached the vault and got rich!"
            : "Game Over.");
    }

    private void Battle(Enemy enemy)
    {
        while (player.Health > 0 && enemy.Health > 0)
        {
            Console.WriteLine("\n--- Encounter Options ---");
            Console.WriteLine("1. Lower Attack (More Accurate, Less Damage)");
            Console.WriteLine("2. Upper Attack (More Damage, Less Accurate)");
            Console.WriteLine($"Your Health: {player.Health} // Enemy's Health: {enemy.Health}");
            Console.Write("Choose an attack: ");
            var atk = Console.ReadLine();

            if (atk == "1" || atk == "2")
            {
                int dmg = atk == "1" ? player.LowerAttack() : player.UpperAttack();
                enemy.TakeDamage(dmg);
                Console.WriteLine($"You dealt {dmg} damage to {enemy.Name}!");
            }
            else
            {
                Console.WriteLine("Invalid input. You flinched and lose your turn.");
            }

            if (enemy.Health <= 0) break;
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            int ed = enemy.Attack();
            player.TakeDamage(ed);
            Console.WriteLine($"{enemy.Name} dealt {ed} damage to you!");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        if (player.Health <= 0)
            Console.WriteLine("Game Over.");
    }
}
