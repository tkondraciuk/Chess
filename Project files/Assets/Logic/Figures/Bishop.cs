using System.Collections.Generic;

namespace Szachy
{
    class Bishop : Figure
    {
        internal Bishop(Color color, Position position) 
            : base(color, position)
        {
        }

        
        public override List<Move> calculateMoves()
        {
            List<Move> moves = new List<Move>();
            bool breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.X++, p.Y++)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.X--, p.Y++)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.X++, p.Y--)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.X--, p.Y--)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            return moves;
        }

        
    }
}
