// A simple collectible Key gained throughout play
public class Key : ICollectible
{
    public string Name { get; private set; }
    public Key() => Name = "Key";
}

// Base class for all weapons and upcoming weapons
public abstract class Weapon : ICollectible
{
    public string Name { get; private set; }
    public double BonusPercent { get; private set; }

    protected Weapon(string name, double bonusPercent)
    {
        Name = name;
        BonusPercent = bonusPercent;
    }
}

// Knife: +5% damage
public class Knife : Weapon
{
    public Knife() : base("Knife", 0.05) { }
}

// Longblade: +20% damage
public class Longblade : Weapon
{
    public Longblade() : base("Longblade", 0.20) { }
}
