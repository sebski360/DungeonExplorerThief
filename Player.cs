using System;
using System.Linq;

class Player : IDamageable
{
    public string Name { get; private set; }
    public int Health { get; private set; }

    private Inventory inventory = new Inventory();
    private Random random = new Random();

    public Player(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void PickUpItem(ICollectible item)
    {
        inventory.AddItem(item);
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
        Console.WriteLine("Inventory Slots:");
        var items = inventory.Items.ToList();
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(i < items.Count
                ? $"{i + 1}. {items[i].Name}"
                : $"{i + 1}. Empty");
        }
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n--- Inventory Menu ---");
        var items = inventory.Items.ToList();
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(i < items.Count
                ? $"{i + 1}. {items[i].Name}"
                : $"{i + 1}. Empty");
        }

        Console.WriteLine("\nOptions:");
        Console.WriteLine("F. Filter strongest weapon");
        Console.WriteLine("S. Sort inventory by name");
        Console.Write("Enter option (or press Enter to go back): ");
        var input = Console.ReadLine();

        if (input.Equals("F", StringComparison.OrdinalIgnoreCase))
        {
            var w = inventory.GetStrongestWeapon();
            if (w != null)
                Console.WriteLine($"Strongest weapon: {w.Name} (+{w.BonusPercent * 100}% attack)");
            else
                Console.WriteLine("No weapons in inventory.");
        }
        else if (input.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Inventory sorted by name:");
            foreach (var item in inventory.SortByName())
                Console.WriteLine(item.Name);
        }
    }

    public int LowerAttack()
    {
        int baseDmg = random.Next(5, 15);
        var w = inventory.GetStrongestWeapon();
        if (w != null) baseDmg = (int)(baseDmg * (1 + w.BonusPercent));
        return baseDmg;
    }

    public int UpperAttack()
    {
        int baseDmg = random.Next(10, 30);
        var w = inventory.GetStrongestWeapon();
        if (w != null) baseDmg = (int)(baseDmg * (1 + w.BonusPercent));
        return baseDmg;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
}
