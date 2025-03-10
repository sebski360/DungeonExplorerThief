using System;
using System.Collections.Generic;

class Room
{
    public string Description { get; private set; }
    public bool HasEnemy { get; private set; }
    public bool HasKey { get; private set; }
    public bool HasHealingItem { get; private set; }
    private static Random random = new Random();

    private static List<EnemyClass> enemyClasses = new List<EnemyClass>
    {
        // Description, Health, Attack Damage, Critical Damage Chance in %
        new EnemyClass("Patrol Officer", 50, 10, 10),
        new EnemyClass("Security Dog", 40, 12, 15),
        new EnemyClass("Elite Guard", 60, 18, 12),
        new EnemyClass("Bank Manager", 45, 9, 8),
        new EnemyClass("Security Turret", 70, 16, 12),
        new EnemyClass("Heavy Tactical Officer", 100, 12, 9)
    };

    public Room(string description, bool hasEnemy, bool hasKey, bool hasHealingItem)
    {
        Description = description;
        HasEnemy = hasEnemy;
        HasKey = hasKey;
        HasHealingItem = hasHealingItem;
    }

    public static List<Room> InitializeRooms()
    {
        return new List<Room>
        {
            new Room("A dark street with an odd looking streetlight.", false, false, false),
            new Room("Tunnel entrance to the vault infrastructure.", true, false, false),
            new Room("A storage room from what it seems like to be.", false, true, false),
            new Room("An empty room with a bunch of posters.", false, false, true),
            new Room("Getting closer.. You see jail bars to the sides with a door at the end of the room.", true, false, false),
            new Room("Inside the vault room. Vault door is upon you", true, false, false)
        };
    }

    public Enemy SpawnEnemy()
    {
        EnemyClass enemyClass = enemyClasses[random.Next(enemyClasses.Count)];
        return new Enemy(enemyClass.Name, enemyClass.Health, enemyClass.AttackDamage, enemyClass.CritChance);
    }

    public string GetDescription()
    {
        return Description;
    }
}

class Enemy
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    private int attackDamage;
    private int critChance;
    private Random random = new Random();

    public Enemy(string name, int health, int attackDamage, int critChance)
    {
        Name = name;
        Health = health;
        this.attackDamage = attackDamage;
        this.critChance = critChance;
    }

    public int Attack()
    {
        bool isCrit = random.Next(100) < critChance;
        int damage = isCrit ? attackDamage * 2 : attackDamage;
        Console.WriteLine(isCrit ? "Enemy lands a Critical Hit! Unlucky!!" : "Enemy attacks.");
        return damage;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
}

class EnemyClass
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int AttackDamage { get; private set; }
    public int CritChance { get; private set; }

    public EnemyClass(string name, int health, int attackDamage, int critChance)
    {
        Name = name;
        Health = health;
        AttackDamage = attackDamage;
        CritChance = critChance;
    }
}
