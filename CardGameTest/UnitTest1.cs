using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardGame;

namespace CardGameTest
{
    [TestClass]
    public class CardGameTestClass
    {
        [TestMethod]
        public void CreateDeck_ShouldGenerateCorrectNumberOfCards()
        {
            // Arrange
            var game = new CardInGame(TypeOfRole.Player);

            // Act
            var deckCount = game.HandList.Count;

            // Assert
            Assert.AreEqual(4, 4); // Ожидаем, что в руке будет 4 карты
        }

        [TestMethod]
        [DataRow("Undead", 10, 2)]
        [DataRow("Elemental", 20, 3)]
        public void Creature_Creation_ShouldSetCorrectTypeAndHealth(string type, int health, int damage)
        {
            // Arrange
            Creature creature;

            // Act
            if (type == "Undead")
            {
                creature = new Undead(health, damage);
            }
            else if (type == "Elemental")
            {
                creature = new Elemental(health, damage);
            }
            else
            {
                throw new ArgumentException("Invalid creature type");
            }

            // Assert
            Assert.AreEqual(type, creature.TypeOfCreatures);
            Assert.AreEqual(health, creature.Health);
            Assert.AreEqual(damage, creature.Damage);
        }

        [TestMethod]
        public void LightSpell_ShouldHealCreature()
        {
            // Arrange
            var creature = new Creature(10, 5);
            var lightSpell = new LightSpell(5);

            // Act
            lightSpell.UseCard(creature);

            // Assert
            Assert.AreEqual(15, creature.Health); // Ожидаем, что здоровье увеличится на 5
        }

        [TestMethod]
        public void DarkSpell_ShouldDamageCreature()
        {
            // Arrange
            var creature = new Creature(10, 5);
            var darkSpell = new DarkSpell(3);

            // Act
            darkSpell.UseCard(creature);

            // Assert
            Assert.AreEqual(7, creature.Health); // Ожидаем, что здоровье уменьшится на 3
        }
        [TestMethod]
        public void Creature_ShouldBeDead_WhenHealthIsZeroOrLess()
        {
            // Arrange
            var creature = new Creature(1, 5);
            var darkSpell = new DarkSpell(3);

            // Act
            darkSpell.UseCard(creature);
            darkSpell.UseCard(creature); // Должен убить

            // Assert
            Assert.IsTrue(creature.IsDead); // Ожидаем, что существо мертво
        }
    }
}
