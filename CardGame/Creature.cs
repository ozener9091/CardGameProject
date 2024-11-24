using System;
using System.Diagnostics;

namespace CardGame
{
    [Serializable]
    public class Creature : Card
    {
        public int Damage { set; get; }
        public int Health { set; get; }
        public int ReadyToAttack { set; get; } = 0;
        public string TypeOfCreatures { set; get; } = "MAIN_CARD";
        public bool IsDead { get; set; } = false;


        public Creature(int health, int damage)
        {
            Damage = damage;
            Health = health;
            TypeOfCard = TypeOfCard.Creature;
        }

        public override void UseCard(Creature enemy)
        {
            Console.WriteLine($"{TypeOfCreatures} атаковал {enemy.TypeOfCreatures} (-{Damage} HP)");
            enemy.Health -= Damage;
            if (enemy.Health <= 0)
            {
                CreatureIsDead(enemy);
            }
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCreatures}    HP: {Health}   Damage: {Damage}");
        }

        static public void CreatureIsDead(Creature enemy)
        {
            enemy.IsDead = true;
            Console.WriteLine($"{enemy.TypeOfCreatures} умер");
            if (enemy.TypeOfCreatures == "MAIN_CARD")
            {
                Process.GetCurrentProcess().Kill();
            }
        }
    }
    [Serializable]
    public class Undead : Creature
    {

        public Undead(int health, int damage) : base(health, damage)
        {
            TypeOfCreatures = "Undead";
        }

        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCreatures}       HP: {Health}    Damage: {Damage}");
        }
    }
    [Serializable]
    public class Elemental : Creature
    {
        public Elemental(int health, int damage) : base(health, damage)
        {
            TypeOfCreatures = "Elemental";
            Health = health * 2;
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCreatures}    HP: {Health}    Damage: {Damage}");
        }
    }
    [Serializable]
    class Beast : Creature
    {
        public Beast(int health, int damage) : base(health, damage)
        {
            TypeOfCreatures = "Beast";
            Damage = damage * 2;
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCreatures}        HP: {Health}    Damage: {Damage}");
        }
    }

    [Serializable]
    class Murlok : Creature
    {
        public Murlok(int health, int damage) : base(health, damage)
        {
            TypeOfCreatures = "Murlok";
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCreatures}       HP: {Health}    Damage: {Damage}");
        }
    }
}
