class Enemy
{
    public string Name { get; set; }
    public int Health { get; private set; }
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
}