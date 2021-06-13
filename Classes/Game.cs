using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;

namespace TicTacToe.Classes
{
    public class Game : GameModel
    {
        public Game() { }
        public Game(int id, string name)
        {
            GameId = id;
            IsStarted = IsFull = IsEnded = false;
            Name = name;
            MoveNumber = 0;
        }

        private const int FieldSize = 3;

        public bool IsFull { get; set; }
        public bool IsStarted { get; set; }
        public bool IsEnded { get; set; }
        private int MoveNumber { get; set; }


        public int[,] Field = new int[FieldSize, FieldSize];


        private bool isFirstMoveNow = true;

        public int CheckGameEnd()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                bool winInRow = true;
                for (int j = 0; j < FieldSize; j++)
                    if (Field[i, 0] == 0 || Field[i, j] != Field[i, 0])
                        winInRow = false;

                if (winInRow)
                    return Field[i, 0];
            }

            for (int j = 0; j < FieldSize; j++)
            {
                bool winInCol = true;
                for (int i = 0; i < FieldSize; i++)
                    if (Field[0, j] == 0 || Field[i, j] != Field[0, j])
                        winInCol = false;

                if (winInCol)
                    return Field[0, j];
            }

            {
                bool winInDiag = true;
                for (int i = 0; i < FieldSize; i++)
                {
                    if (Field[0, 0] == 0 || Field[i, i] != Field[0, 0])
                        winInDiag = false;
                }

                if (winInDiag)
                    return Field[0, 0];
            }

            {
                bool winInAntiDiag = true;
                for (int i = 0; i < FieldSize; i++)
                {
                    if (Field[0, FieldSize - 1] == 0 || Field[i, FieldSize - i - 1] != Field[0, FieldSize - 1])
                        winInAntiDiag = false;
                }
                if (winInAntiDiag)
                    return Field[0, FieldSize - 1];
            }

            {
                bool isDeadHeat = true;
                for (int i = 0; i < FieldSize; i++)
                    for (int j = 0; j < FieldSize; j++)
                        if (Field[i, j] == 0)
                            isDeadHeat = false;

                if (isDeadHeat)
                    return 3;
            }
            return 0;
        }
        public int MakeMove(int i, int j, string player)
        {
            if (player == "First" && !isFirstMoveNow)
                return 0;

            if (player == "Second" && isFirstMoveNow)
                return 0;

            if (Field[i, j] != 0)
                return 0;

            Field[i, j] = isFirstMoveNow ? 1 : 2;
            isFirstMoveNow = !isFirstMoveNow;
            MoveNumber++;
            return isFirstMoveNow ? 2 : 1;
        }
        public int TryStart()
        {
            if (!IsStarted)
            {
                IsStarted = true;
                return 0;
            }
            if (IsStarted && !IsFull && !IsEnded)
            {
                IsFull = true;
                return 1;
            }
            return 2;
        }

        public (int, int)? GetFirstMove()
        {
            if (MoveNumber == 1)
            {
                for (int i = 0; i < FieldSize; i++)
                    for (int j = 0; j < FieldSize; j++)
                        if (Field[i, j] != 0)
                            return (i, j);
            }
            return null;
        }
    }
}
