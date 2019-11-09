using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Szachy
{
    class King : Figure
    {
        bool hasMoved = false;
        internal King(Color color, Position position) 
            : base(color, position)
        {
        }

        public override List<Move> calculateMoves()
        {
            List<Move> moves = new List<Move>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j<=1; j++)
                {
                    Position p = new Position(Position.X + i, Position.Y + j);
                    if (!p.Equals(Position))
                    {
                        Move move = getMove(p);
                        if (move != null) moves.Add(move);
                    }
                }
            }
            moves.AddRange(getPossibleCastlings());
            return moves;
        }

        public override List<Move> getPossibleMoves()
        {
            return calculateMoves().Where(m => !isCheckOnPosition(m.Destination)).ToList();
        }

        protected override Move getMove(Position position)
        {
            return base.getMove(position);
            
        }

        private List<Move> getPossibleCastlings()
        {
            if (hasMoved || MainController.KingInCheck==this) return new List<Move>();
            Position p1 = null;
            Position p2 = null;

            if (Color == Color.White) { p1 = "A1"; p2 = "H1"; }
            else { p1 = "A8"; p2 = "H8"; }

            Castling[] castlings =
            {
                new Castling(Position[-2,0],this,getRookForCastling(p1),p1[3,0]),
                new Castling(Position[2,0],this,getRookForCastling(p2),p2[-2,0])
            };
            return castlings
                .Where(c => c.Rook != null)
                .Cast<Move>()
                .ToList();
           
            
        }

        private Rook getRookForCastling(Position p)
        {
            Figure figure = Board.GetFigure(p);
            if (!(figure is Rook)) return null;

            Rook rook = figure as Rook;
            if (rook.HasMoved) return null;

            int step = Math.Sign(p.X - Position.X);
            for (Position i = Position[step,0]; !p.Equals(i); i=i[step,0])
            {
                if (Board.GetFigure(i) != null) return null;
            }

            return rook;
        }

        private bool isCheckOnPosition(Position p)
        {
            Position currentPosition = new Position(Position);
            Figure figureOnPosition = Board.GetFigure(p);
            bool currentHasMoved = hasMoved;

            setPosition(p);
            if (figureOnPosition != null) Board.RemoveFigure(figureOnPosition);
            bool isCheckingPosition = isInCheck();

            setPosition(currentPosition);
            hasMoved = currentHasMoved;
            if (figureOnPosition != null) Board.AddFigure(figureOnPosition);
            return isCheckingPosition;
            
        }

        public bool isInCheck()
        {
            Figure[] figures = Board.GetFigures(Color.Invert());
            return figures
                .Any(f => f.canKill(this));
        }

        public override void setPosition(Position position)
        {
            base.setPosition(position);
            hasMoved = true;
        }
    }
}
