using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardGame
{
    [Serializable]
    public class GameState
    {
        public List<Card> Deck { get; set; }
        public List<Card> Hand { get; set; }
        public List<Creature> Board { get; set; }
        public TypeOfRole Role { get; set; }
    }

    static public class Save
    {
        private const string SAVE_FILE_PLAYER = "gameSavePlayer.dat";
        private const string SAVE_FILE_ENEMY = "gameSaveEnemy.dat";

        //Сохранение игры
        static public void SaveGame(CardInGame player, CardInGame enemy)
        {

            var gameStatePlayer = new GameState
            {
                Deck = player.DeckList,
                Hand = player.HandList,
                Board = player.BoardList,
                Role = player.TypeOfRole
            };
            var gameStateEnemy = new GameState
            {
                Deck = enemy.DeckList,
                Hand = enemy.HandList,
                Board = enemy.BoardList,
                Role = enemy.TypeOfRole
            };
            using (FileStream stream = new FileStream(SAVE_FILE_PLAYER, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, gameStatePlayer);
            }
            using (FileStream stream = new FileStream(SAVE_FILE_ENEMY, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, gameStateEnemy);
            }

        }
        
    }

    static public class Load
    {
        private const string LOAD_FILE_PLAYER = "gameSavePlayer.dat";
        private const string LOAD_FILE_ENEMY = "gameSaveEnemy.dat";
        //Загрузка игры
        static public void LoadGame(CardInGame player, CardInGame enemy)
        {

            if (!File.Exists(LOAD_FILE_PLAYER) || !File.Exists(LOAD_FILE_ENEMY)) return;

            using (FileStream stream = new FileStream(LOAD_FILE_PLAYER, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var gameStatePlayer = (GameState)formatter.Deserialize(stream);

                player.DeckList = gameStatePlayer.Deck;
                player.HandList = gameStatePlayer.Hand;
                player.BoardList = gameStatePlayer.Board;
                player.TypeOfRole = gameStatePlayer.Role;

            }
            using (FileStream stream = new FileStream(LOAD_FILE_ENEMY, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var gameStateEnemy = (GameState)formatter.Deserialize(stream);

                enemy.DeckList = gameStateEnemy.Deck;
                enemy.HandList = gameStateEnemy.Hand;
                enemy.BoardList = gameStateEnemy.Board;
                enemy.TypeOfRole = gameStateEnemy.Role;

            }
        }
    }
}
