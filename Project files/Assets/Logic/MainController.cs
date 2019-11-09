using System;
using System.Collections.Generic;
using UnityEngine;

namespace Szachy
{
    static class MainController
    {
        static Board board = new Board();
        static Color turn = Color.White;
        static Player whitePlayer = new Player(Color.White);
        static Player blackPlayer = new Player(Color.Black);
        static Dictionary<Position, Vector3> unityCords = new Dictionary<Position, Vector3>();
        static Figure selectedFigure;
        const float deltaX = 3.02f;
        const float deltaY = 3f;
        internal static Board Board
        {
            get
            {
                return board;
            }

            set
            {
                board = value;
            }
        }
        internal static Color Turn
        {
            get
            {
                return turn;
            }

            set
            {
                turn = value;
            }
        }
        internal static Player WhitePlayer
        {
            get
            {
                return whitePlayer;
            }

            set
            {
                whitePlayer = value;
            }
        }
        internal static Player BlackPlayer
        {
            get
            {
                return blackPlayer;
            }

            set
            {
                blackPlayer = value;
            }
        }
        internal static King KingInCheck
        {
            get { return Board.KingInCheck; }
        }
        internal static King KingInMate
        {
            get { return Board.KingInMate; }
        }

        internal static Dictionary<Position, Vector3> UnityCords
        {
            get
            {
                return unityCords;
            }

            set
            {
                unityCords = value;
            }
        }

        internal static Figure SelectedFigure
        {
            get
            {
                return selectedFigure;
            }

            set
            {
                selectedFigure = value;
            }
        }

        internal static void InitializeGame()
        {
            Board.InitializeBoard();
            Turn = Color.White;
            WhitePlayer.setKing();
            BlackPlayer.setKing();
        }
        internal static void NextTurn()
        {
            switch (Turn)
            {
                case Color.Black:
                    Turn = Color.White;
                    break;
                case Color.White:
                    Turn = Color.Black;
                    break;
                default:
                    break;
            }
        }

        public static void InitializeUnityCords()
        {
            Position p = "A1";
            Vector3 v = new Vector3(-10.59f, 0.07f, -10.46f);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Vector3 v2 = new Vector3(i * deltaX, 0f, j * deltaY);
                    UnityCords.Add(p[i, j], v + v2);
                }
            }
        }

        

        public static Color Invert(this Color color)
        {
            if (color == Color.White) return Color.Black;
            else return Color.White;
        }

        
    }
}
