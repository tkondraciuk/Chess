using System.Collections.Generic;

namespace Szachy
{
    class Rook : Figure
    {
        bool hasMoved = false;
        internal Rook(Color color, Position position) 
            : base(color, position)
        {
        }

        public bool HasMoved
        {
            get
            {
                return hasMoved;
            }

            set
            {
                hasMoved = value;
            }
        }

        public override List<Move> calculateMoves()
        {
            List<Move> moves = new List<Move>();
            bool breakLoop = false;
            for (Position p=new Position(Position); p.isValid() && !breakLoop; p.X++)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.X--)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.Y++)
            {
                if (!p.Equals(Position))
                {
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);
                    if (move == null || move is Attack) breakLoop = true;
                }
            }

            breakLoop = false;
            for (Position p = new Position(Position); p.isValid() && !breakLoop; p.Y--)
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

        public override void setPosition(Position position)
        {
            base.setPosition(position);
            HasMoved = true;
        }

        public override List<Move> getPossibleMoves()
        {
            bool currentHasMoved = hasMoved;
            List<Move> result = base.getPossibleMoves();
            hasMoved = currentHasMoved;
            return result;
            
        }

    }
}
