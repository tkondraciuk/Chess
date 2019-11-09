using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Szachy
{
    abstract class Figure
    {
        private Board board;
        private Color color;
        private List<Move> possibleMoves = new List<Move>();
        private Position position;

        internal Board Board
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
        internal Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
        internal List<Move> PossibleMoves
        {
            get
            {
                return possibleMoves;
            }

            set
            {
                possibleMoves = value;
            }
        }
        internal Position Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        internal Figure(Color color,  Position position)
        {
            this.Color = color;
            this.Position = position;
            this.Board = MainController.Board;
        }

        public virtual List<Move> getPossibleMoves()
        {
            List<Move> moves = calculateMoves();
            King king = Board.GetFigures(Color, FiguresEnum.King).FirstOrDefault() as King;
            List<Move> movesInCheck = new List<Move>();
            foreach (var move in moves)
            {
                Position p = move.Destination;
                Position currentPosition = new Position(Position);
                Figure figureOnPosition = Board.GetFigure(p);
                if (figureOnPosition != null) Board.RemoveFigure(figureOnPosition);
                setPosition(p);
                if (!king.isInCheck()) movesInCheck.Add(move);
                setPosition(currentPosition);
                if (figureOnPosition != null) Board.AddFigure(figureOnPosition);
            }
            return movesInCheck;


        }

        abstract public List<Move> calculateMoves();

        public void SavePossibleMoves(List<Move> possibleMoves=null)
        {
            if (possibleMoves == null) possibleMoves = getPossibleMoves();
           
            PossibleMoves = possibleMoves;
        }

        public virtual void setPosition(Position position)
        {
            Position = position;
        }

         protected virtual Move getMove(Position position)
        {
            if (!position.isValid()) return null;
            Figure f = Board.GetFigure(position);
            if (f == null) { return new Move(position, this); }
            if (f.Color != this.Color) { return new Attack(position, this, f); }
            return null;
        }

         public bool canKill(Figure figure)
        {
            return calculateMoves()
                .Where(m => m is Attack)
                .Select(m => m as Attack)
                .Any(a => a.Target == figure);
        }

       




        public override string ToString()
        {
            string typ = GetType().Name;
            return Color.ToString()+" "+typ + "[" + Position + "]";
        }
    }
}
