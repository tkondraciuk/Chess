using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Szachy
{
    class Board
    {
        List<Figure> figures = new List<Figure>();
         readonly static char[] columnTags = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
         readonly int[] rowTags = { 1, 2, 3, 4, 5, 6, 7, 8 };
        readonly FiguresEnum[] figuresInOrder = { FiguresEnum.Rook, FiguresEnum.Knight, FiguresEnum.Bishop, FiguresEnum.Queen, FiguresEnum.King, FiguresEnum.Bishop, FiguresEnum.Knight, FiguresEnum.Rook };
        King kingInCheck = null;
        King kingInMate = null;
        GameObject boardObject;

        

        internal List<Figure> Figures
        {
            get
            {
                return figures;
            }

            set
            {
                figures = value;
            }
        }
        internal King KingInCheck
        {
            get
            {
                return kingInCheck;
            }

            set
            {
                kingInCheck = value;
            }
        }
        internal King KingInMate
        {
            get
            {
                return kingInMate;
            }

            set
            {
                kingInMate = value;
            }
        }

        public GameObject BoardObject
        {
            get
            {
                return boardObject;
            }

            set
            {
                boardObject = value;
            }
        }

       

       

        public Figure GetFigure(Position position)
        {
            return Figures
                .Where(f => f.Position.Equals(position))
                .FirstOrDefault();

        }
        public Figure[] GetFigures(Color color)
        {
            return Figures.Where(f => f.Color == color).ToArray();
        }
        public Figure[] GetFigures(FiguresEnum type)
        {
            string typeString = type.ToString();
            return Figures
                .Where(f => f.GetType().Name == typeString)
                .ToArray();
        }
        public Figure[] GetFigures(Color color, FiguresEnum type)
        {
            string typeString = type.ToString();
            return Figures
                .Where(f => f.Color == color && f.GetType().Name == typeString)
                .ToArray();
        }

        internal void AddFigure(Figure figure)
        {
            Figures.Add(figure);
        }

        public void AddFigure(FiguresEnum figure, char column, int row, Color color)
        {
            Position position = new Position(column, row);
            AddFigure(figure, position, color);
        }
        public Figure AddFigure(FiguresEnum figure, Position position, Color color)
        {
            switch (figure)
            {
                case FiguresEnum.Rook:
                    Rook r = new Rook(color, position);
                    Figures.Add(r);
                    return r;
                case FiguresEnum.Bishop:
                    Bishop b = new Bishop(color, position);
                    Figures.Add(b);
                    return b;
                case FiguresEnum.Queen:
                    Queen q = new Queen(color, position);
                    Figures.Add(q);
                    return q;
                case FiguresEnum.King:
                    King k = new King(color, position);
                    Figures.Add(k);
                    return k;
                case FiguresEnum.Knight:
                    Knight kn = new Knight(color, position);
                    Figures.Add(kn);
                    return kn;
                case FiguresEnum.Pawn:
                    Pawn p = new Pawn(color, position);
                    Figures.Add(p);
                    return p;
                default:
                    return null;
            }

        }
        public void RemoveFigure(Figure figure)
        {
            Figures.Remove(figure);
        }
        public bool isCheck()
        {
           
            King whiteKing = GetFigures(Color.White, FiguresEnum.King).FirstOrDefault() as King;
            King blackKing = GetFigures(Color.Black, FiguresEnum.King).FirstOrDefault() as King;
            if(whiteKing!=null && whiteKing.isInCheck())
            {
                KingInCheck = whiteKing;
                isMate();
                return true;
            }
            if (blackKing!=null && blackKing.isInCheck())
            {
                KingInCheck = blackKing;
                isMate();
                return true;
            }
            KingInCheck = null;
            return false;

        }

        public bool isMate()
        {
            bool isMate = true;
            foreach (var figure in GetFigures(KingInCheck.Color))
            {
                if (figure.getPossibleMoves().Count>0) isMate = false;
            }
            if (isMate) KingInMate = KingInCheck;
            return isMate;
        }
        

        

        public void InitializeBoard()
        {
            int i = 0;
            foreach (var column in columnTags)
            {
                AddFigure(figuresInOrder[i], column, 1, Color.White);
                AddFigure(FiguresEnum.Pawn, column, 2, Color.White);

                AddFigure(figuresInOrder[i], column, 8, Color.Black);
                AddFigure(FiguresEnum.Pawn, column, 7, Color.Black);

                i++;
            }
        }

       
        
    }
}
