using System;

namespace CardGame
{
    internal class Program
    {

        static int numberCardPlayer;
        static int numberCardEnemy;
        static string numberCardStr;
        static int healCardIndex;
        static int countOfMove = 0;
        static Random rng = new Random();


        //Создаем набор карт для игрока и соперника
        static CardInGame playerCards = new CardInGame(TypeOfRole.Player);
        static CardInGame enemyCards = new CardInGame(TypeOfRole.Enemy);


        //Вывод информации о состоянии стола
        static void PrintBoardInfo()
        {
            playerCards.PrintBoard();
            enemyCards.PrintBoard();
        }


        //Ход противника (расстановка карт)
        static void EnemyTurn()
        {
            Console.WriteLine("---------Ход противника---------\n");


            //Если у противника есть карта на руке, то он ее выкладывает
            if (enemyCards.HandList.Count != 0)
            {
                //Генерит рандомную карту которой походит противник
                numberCardEnemy = rng.Next(0, enemyCards.HandList.Count);

                //Выбор действия в зависимости от типа карты
                switch (enemyCards.HandList[numberCardEnemy].TypeOfCard)
                {
                    case TypeOfCard.Creature:
                        //Противник выкладывает карту на стол если это существо
                        enemyCards.AddOnBoard(numberCardEnemy);
                        break;

                    case TypeOfCard.DarkSpell:

                        //Выбирает на какую карту игрока использовать спел
                        numberCardPlayer = rng.Next(0, enemyCards.BoardList.Count);

                        enemyCards.HandList[numberCardEnemy].UseCard(playerCards.BoardList[numberCardPlayer]);

                        //Если спел убил существо игрока
                        if (playerCards.BoardList[numberCardPlayer].IsDead)
                        {
                            playerCards.RemoveCardFromBoard(numberCardPlayer);
                        }

                        //Удаляем спел из руки соперника
                        enemyCards.RemoveCardFromHand(numberCardEnemy);

                        break;
                    //Если Light spell то хилит свою карту
                    case TypeOfCard.LightSpell:

                        //Выбирает какую карту отхилить
                        healCardIndex = rng.Next(0, enemyCards.BoardList.Count);

                        enemyCards.HandList[numberCardEnemy].UseCard(enemyCards.BoardList[healCardIndex]);
                        //Удаляем спел из руки соперника
                        enemyCards.RemoveCardFromHand(numberCardEnemy);
                        break;
                }
            }

            Console.WriteLine("\n\n");

        }

        //Ход противника (атака)
        static void EnemyAttack()
        {
            //Считаем сколько раз может походить
            foreach (var card in enemyCards.BoardList)
            {
                countOfMove += card.ReadyToAttack;
            }

            while (countOfMove > 0)
            {

                PrintBoardInfo();

                //Проверка что карта еще не ходила
                while (true)
                {
                    //Соперник выбирает карту для атаки
                    numberCardEnemy = rng.Next(0, enemyCards.BoardList.Count);
                    if (enemyCards.BoardList[numberCardEnemy].ReadyToAttack == 1)
                    {
                        break;
                    }
                }

                //Соперник выбирает карту игрока для атаки
                numberCardPlayer = rng.Next(0, playerCards.BoardList.Count);

                enemyCards.BoardList[numberCardEnemy].UseCard(playerCards.BoardList[numberCardPlayer]);

                //Если атака убила карту игрока
                if (playerCards.BoardList[numberCardPlayer].IsDead)
                {
                    playerCards.RemoveCardFromBoard(numberCardPlayer);
                }

                //Опускаем флаг готовности к атаке походившей карты
                enemyCards.BoardList[numberCardEnemy].ReadyToAttack = 0;
                countOfMove--;
            }

            //Возвращаем готовность карт на доске к атаке перед след ходом
            for (int i = 0; i < enemyCards.BoardList.Count; i++)
            {
                enemyCards.BoardList[i].ReadyToAttack = 1;
            }
        }

        //Ход игрока (расстанвока карт)
        static void PlayerTurn()
        {

            //Если на руке остались карты
            if (playerCards.HandList.Count != 0)
            {
                playerCards.PrintHand();

                Console.WriteLine("0 - не использовать");
                Console.WriteLine("\nВведите номер карты которую хотите использовать ");


                //Игрок выбирает карту
                numberCardPlayer = GetNumber.Int() - 1;

                //Проверка если выбрал '0'
                if (numberCardPlayer != -1)
                {
                    //Выбор действия в зависимости от типа карты
                    switch (playerCards.HandList[numberCardPlayer].TypeOfCard)
                    {
                        case TypeOfCard.Creature:
                            //Выкладываем карту существа на стол
                            playerCards.AddOnBoard(numberCardPlayer);
                            break;

                        case TypeOfCard.DarkSpell:

                            foreach (var card in enemyCards.BoardList)
                            {
                                card.PrintInfo();
                            }
                            Console.WriteLine("Выберите карту соперника : ");

                            //Игрок выбирает карту соперника
                            numberCardEnemy = GetNumber.Int() - 1;

                            playerCards.HandList[numberCardPlayer].UseCard(enemyCards.BoardList[numberCardEnemy]);

                            //Если спел убил карту соперника
                            if (enemyCards.BoardList[numberCardEnemy].IsDead)
                            {
                                enemyCards.RemoveCardFromBoard(numberCardEnemy);
                            }
                            //Удаляем спел из руки игрока
                            playerCards.RemoveCardFromHand(numberCardPlayer);
                            break;

                        case TypeOfCard.LightSpell:

                            foreach (var card in playerCards.BoardList)
                            {
                                card.PrintInfo();
                            }
                            Console.WriteLine("Выберите свою карту для лечения: ");

                            //Игрок выбирает карту для лечения
                            healCardIndex = GetNumber.Int() - 1;

                            playerCards.HandList[numberCardPlayer].UseCard(playerCards.BoardList[healCardIndex]);

                            //Удаляем спел из руки игрока
                            playerCards.RemoveCardFromHand(numberCardPlayer);
                            break;
                    }
                }
            }

        }

        //Ход игрока (атака)
        static void PlayerAttack()
        {
            //Считаем сколько раз можем походить
            foreach (var card in playerCards.BoardList)
            {
                countOfMove += card.ReadyToAttack;
            }

            while (countOfMove > 0)
            {

                PrintBoardInfo();


                //Выводим карты игрока которыми он может атаковать
                foreach (var card in playerCards.BoardList)
                {
                    if (card.ReadyToAttack == 1)
                    {
                        card.PrintInfo();
                    }
                }

                Console.Write("Выберите свою карту для атаки: ");

                //Игрок выбирает карту для хода
                numberCardPlayer = GetNumber.Int() - 1;

                Console.Write("Выберите карту соперника для атаки: ");

                //Игрок выбирает карту соперника для атаки
                numberCardEnemy = GetNumber.Int() - 1;


                playerCards.BoardList[numberCardPlayer].UseCard(enemyCards.BoardList[numberCardEnemy]);

                //Если атака убила карту соперника
                if (enemyCards.BoardList[numberCardEnemy].IsDead)
                {
                    enemyCards.RemoveCardFromBoard(numberCardEnemy);
                }

                //Опускаем флаг готовности к атаке походившей карты
                playerCards.BoardList[numberCardPlayer].ReadyToAttack = 0;
                countOfMove--;
            }

            //Возвращаем готовность карт на доске к атаке перед след ходом
            for (int i = 0; i < playerCards.BoardList.Count; i++)
            {
                playerCards.BoardList[i].ReadyToAttack = 1;
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Начать игру с сохранения [Y] Начать игру заново [N] : ");
            if(Console.ReadLine() == "Y")
            {
                Load.LoadGame(playerCards, enemyCards);
            }
            //Главный цикл игровой схватки 
            while (true)
            {

                Save.SaveGame(playerCards, enemyCards);

                PrintBoardInfo();

                EnemyTurn();
                EnemyAttack();

                PrintBoardInfo();

                PlayerTurn();
                PlayerAttack();

                playerCards.AddOnHandFromDeck();
                enemyCards.AddOnHandFromDeck();

            }
        }
    }
}
