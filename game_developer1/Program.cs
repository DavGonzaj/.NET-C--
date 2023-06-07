class Attack
{
    public string Name { get; }
    public int DamageAmount { get; }

    public Attack(string name, int damageAmount)
    {
        Name = name;
        DamageAmount = damageAmount;
    }
}

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

class Program
{
    static void Main()
    {
        // Create an instance of an Enemy
        Enemy enemy = new Enemy("Bandit");

        // Create 3 instances of Attacks
        Attack fireball = new Attack("Fireball", 20);
        Attack punch = new Attack("Punch", 15);
        Attack throwAttack = new Attack("Throw", 10);

        // Add the three Attacks to the Enemy's AttackList
        enemy.AttackList.Add(fireball);
        enemy.AttackList.Add(punch);
        enemy.AttackList.Add(throwAttack);

        // Call RandomAttack to test a random Attack
        enemy.RandomAttack();
    }
}