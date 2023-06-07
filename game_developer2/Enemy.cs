class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public List<Attack> AttackList { get; }

    public Enemy(string name)
    {
        Name = name;
        Health = 100;
        AttackList = new List<Attack>();
    }

    public void RandomAttack()
    {
        Random random = new Random();
        int attackIndex = random.Next(AttackList.Count);
        Attack randomAttack = AttackList[attackIndex];
        Console.WriteLine($"The {Name} performs a {randomAttack.Name} attack, dealing {randomAttack.DamageAmount} damage.");
    }
    public void PerformAttack(Enemy target, Attack chosenAttack)
    {
        if (target.Health <= 0)
        {
            Console.WriteLine($"{target.Name} has no health left and cannot be attacked.");
            return;
        }

        target.Health -= chosenAttack.DamageAmount;
        Console.WriteLine($"{Name} attacks {target.Name} with {chosenAttack.Name}, dealing {chosenAttack.DamageAmount} damage.");
        Console.WriteLine($"{target.Name}'s health is now {target.Health}.");

        if (target.Health <= 0)
        {
            Console.WriteLine($"{target.Name} has been defeated!");
        }
    }
}