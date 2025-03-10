using System;

class Player
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    private string inventoryItem;
    private Random random = new Random();

    public Player(string name, int health)
    {
        Name = name;
        Health = health;
        inventoryItem = null;
    }

    public void PickUpItem(string item)
    {
        inventoryItem = item;
        Console.WriteLine($"You picked up: {item}");
    }

    public void Heal(int amount)
    {
        Health += amount;
        Console.WriteLine($"You healed {amount} health. Your health: {Health}");
    }

    public void DisplayStats()
    {
        Console.WriteLine("\n--- Player Stats ---");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Inventory: {(inventoryItem != null ? inventoryItem : "Empty")}");
    }

// Different Attacks
// Makes Encounters more interesting and random.
    public int LowerAttack()
    {
        return random.Next(5, 15); // More reliable damage
    }

    public int UpperAttack()
    {
        return random.Next(10, 30); // Higher risk damage, High gamble High reward
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
}
