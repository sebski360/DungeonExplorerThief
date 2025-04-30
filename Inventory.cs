using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    private List<ICollectible> items = new List<ICollectible>();
    private const int MaxCapacity = 3;

    public bool AddItem(ICollectible item)
    {
        if (items.Count >= MaxCapacity)
        {
            Console.WriteLine($"Inventory is full. Cannot pick up {item.Name}.");
            return false;
        }
        items.Add(item);
        Console.WriteLine($"You picked up: {item.Name}");
        return true;
    }

    public IEnumerable<ICollectible> Items => items;

    public IEnumerable<T> FilterItems<T>() where T : ICollectible
    {
        return items.OfType<T>();
    }

    public IEnumerable<ICollectible> SortByName()
    {
        return items.OrderBy(i => i.Name);
    }

    public Weapon GetStrongestWeapon()
    {
        return items.OfType<Weapon>().OrderByDescending(w => w.BonusPercent).FirstOrDefault();
    }
}
