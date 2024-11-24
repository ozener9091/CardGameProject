using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    public enum TypeOfCard
    {
        Creature,
        DarkSpell,
        LightSpell
    }

    public enum TypeOfRole
    {
        Player,
        Enemy
    }

    [Serializable]
    public abstract class Card
    {
        public TypeOfCard TypeOfCard { get; set; }
        public virtual void PrintInfo() { }
        public virtual void UseCard(Creature enemy) { }

    }

    [Serializable]
    //Класс определяющий все карты игрока-соперника (колода, рука, стол)
    public class CardInGame
    {
       
        static private List<string> TYPE_OF_CREATURES = new List<string> { "Undead", "Elemental", "Beast", "Murlok" };
        static private List<string> TYPE_OF_SPELLS = new List<string> { "DarkSpell", "LightSpell" };
        static private Random rng = new Random();

        private List<Card> _deckList = new List<Card>();
        private List<Creature> _boardList = new List<Creature>();
        private List<Card> _handList = new List<Card>();
        private TypeOfRole _typeOfRole;

        public List<Card> HandList { get { return _handList; } set { _handList = value; } }
        public List<Creature> BoardList { get { return _boardList; } set { _boardList = value; } }
        public List<Card> DeckList { get { return _deckList; } set { _deckList = value; } }
        public TypeOfRole TypeOfRole { get { return _typeOfRole; } set { _typeOfRole = value; } }

        public CardInGame(TypeOfRole typeOfRole)
        {
            _typeOfRole = typeOfRole;

            //Создаем колоду
            CreateDeck();

            //Добавляем на стол главную карту
            _boardList.Add(new Creature(20, 1));

            //Берем 4 карты в руку перед началом игры
            for (int i = 0; i < 4; i++)
            {
                _handList.Add(_deckList[i]);
                _deckList.Remove(_deckList[i]);
            }

        }

        //Геренрация колоды
        private void CreateDeck()
        {
            foreach (var card in TYPE_OF_CREATURES)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (card == "Undead")
                        {
                            _deckList.Add(new Undead(i, j));
                        }
                        if (card == "Elemental")
                        {
                            _deckList.Add(new Elemental(i, j));
                        }
                        if (card == "Beast")
                        {
                            _deckList.Add(new Beast(i, j));
                        }
                        if (card == "Murlok")
                        {
                            _deckList.Add(new Murlok(i, j));
                        }

                    }
                }
            }
            foreach (var card in TYPE_OF_SPELLS)
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (card == "DarkSpell")
                    {
                        _deckList.Add(new DarkSpell(3));
                    }
                    if (card == "LightSpell")
                    {
                        _deckList.Add(new LightSpell(3));
                    }
                }
            }

            //Мешаем карты
            _deckList = _deckList.OrderBy(x => rng.Next()).ToList();
            _deckList = _deckList.OrderBy(x => rng.Next()).ToList();


        }

        //Выложить карту на стол
        public void AddOnBoard(int index)
        {
            _boardList.Add((Creature)HandList[index]);
            Console.WriteLine($"{_typeOfRole} выложил карту {((Creature)HandList[index]).TypeOfCreatures}");
            HandList.Remove(HandList[index]);
        }

        //Взять карту из колоды
        public void AddOnHandFromDeck()
        {
            if (_deckList.Count != 0)
            {
                _handList.Add(_deckList[0]);
                _deckList.Remove(_deckList[0]);
            }
            else
            {
                Console.WriteLine("Колода пуста");
            }
        }

        //Вывести информацию о столе
        public void PrintBoard()
        {
            Console.WriteLine($"\n\n----------Стол {_typeOfRole}----------\n");
            foreach (var card in _boardList)
            {
                card.PrintInfo();
            }
            Console.WriteLine("\n");
        }

        //Вывести информацию о руке
        public void PrintHand()
        {
            Console.WriteLine($"\n\n----------Рука {_typeOfRole}----------\n");
            foreach (var card in _handList)
            {
                card.PrintInfo();
            }
        }

        //Удалить карту из руки
        public void RemoveCardFromHand(int index)
        {
            HandList.Remove(HandList[index]);
        }

        //Удалить карту на доске
        public void RemoveCardFromBoard(int index)
        {
            BoardList.Remove(BoardList[index]);
        }
    }
}
