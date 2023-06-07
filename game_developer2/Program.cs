class RangedFighter : Enemy
{
    public int Distance { get; private set; }

    public RangedFighter() : base("Ranged Fighter")
    {
        Health = 100;
        Distance = 5;

        Attack shootArrow = new Attack("Shoot an Arrow", 20);
        Attack throwKnife = new Attack("Throw a Knife", 15);

        AttackList.Add(shootArrow);
        AttackList.Add(throwKnife);
    }

    public void Dash()
    {
        Distance = 20;
        Console.WriteLine($"{Name} performs a dash. Distance is now {Distance}.");
    }

    public void PerformAttack(Enemy target, Attack chosenAttack)
    {
        if (Distance < 10)
        {
            Console.WriteLine($"{Name} is too close to perform a ranged attack.");
            return;
        }

        base.PerformAttack(target, chosenAttack);
    }
}

class MagicCaster : Enemy
{
    public MagicCaster() : base("Magic Caster")
    {
        Health = 80;

        Attack fireball = new Attack("Fireball", 25);
        Attack lightningBolt = new Attack("Lightning Bolt", 20);
        Attack staffStrike = new Attack("Staff Strike", 10);

        AttackList.Add(fireball);
        AttackList.Add(lightningBolt);
        AttackList.Add(staffStrike);
    }

    public void Heal(Enemy target)
    {
        target.Health += 40;
        Console.WriteLine($"{Name} heals {target.Name} for 40 health. {target.Name}'s health is now {target.Health}.");
    }

}