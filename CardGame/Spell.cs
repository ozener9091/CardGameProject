using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    [Serializable]
    public class LightSpell : Card
    {
        public int Heal { get; }

        public LightSpell(int heal)
        {
            if (heal <= 0) throw new ArgumentException("Поле 'Лечение' Light Spell должно быть положительным");
            Heal = heal;
            TypeOfCard = TypeOfCard.LightSpell;
        }
        public override void UseCard(Creature enemy)
        {
            enemy.Health += Heal;
            Console.WriteLine($"{enemy.TypeOfCreatures} Вылечен на {Heal} HP");
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCard}           Heal:{Heal}");
        }
    }

    [Serializable]
    public class DarkSpell : Card
    {
        public int Damage { get; }

        public DarkSpell(int damage)
        {
            if(damage <= 0) throw new ArgumentException("Поле 'Урон' Dark Spell должно быть положительным");
            Damage = damage;
            TypeOfCard = TypeOfCard.DarkSpell;
        }
        public override void UseCard(Creature enemy)
        {
            enemy.Health -= Damage;
            Console.WriteLine($"{enemy.TypeOfCreatures} получил урон от Dark spell на {Damage} HP");
            if (enemy.Health <= 0)
            {
                Creature.CreatureIsDead(enemy);
            }
        }
        public override void PrintInfo()
        {
            Console.WriteLine($"Type: {TypeOfCard}             Damage:3");
        }
    }
}
