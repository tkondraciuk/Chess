using System.Collections.Generic;

namespace Szachy
{
    class Pawn : Figure
    {
        bool hasMoved = false;
        internal Pawn(Color color, Position position) 
            : base(color, position)
        {
        }

        public override List<Move> calculateMoves()
        {
            List<Move> moves = new List<Move>();
            Move move;
            Move doubleMove;
            Move attack1;
            Move attack2;
            if (Color == Color.White)
            {
                move = getMove(Position[0, 1]);
                doubleMove = getMove(Position[0, 2]);
                attack1 = getMove(Position[-1, 1]);
                attack2 = getMove(Position[1, 1]);
            }
            else
            {
                move = getMove(Position[0, -1]);
                doubleMove = getMove(Position[0, -2]);
                attack1 = getMove(Position[-1, -1]);
                attack2 = getMove(Position[1, -1]);
            }
          
            if (move != null && !(move is Attack)) moves.Add(move);
            if (!hasMoved && doubleMove != null 
                && !(doubleMove is Attack) 
                && moves.Count>0)  moves.Add(doubleMove);
            if (attack1 is Attack) moves.Add(attack1);
            if (attack2 is Attack) moves.Add(attack2);
            return moves;
        }

        public override void setPosition(Position position)
        {
            base.setPosition(position);
            hasMoved = true;
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
