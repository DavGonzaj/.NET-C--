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