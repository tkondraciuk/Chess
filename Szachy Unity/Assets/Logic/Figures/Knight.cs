using System.Collections.Generic;

namespace Szachy
{
    class Knight : Figure
    {
        internal Knight(Color color, Position position) 
            : base(color, position)
        {
        }

        public override List<Move> calculateMoves()
        {
            List<Move> moves = new List<Move>();
            int[] iStates = { -2, 2 };
            int[] jStates = { -1, 1 };

            foreach (var i in iStates)
            {
                foreach (var j in jStates)
                {
                    Position p = new Position(Position.X + i, Position.Y + j);
                    Move move = getMove(p);
                    if (move != null) moves.Add(move);

                    p = new Position(Position.X + j, Position.Y + i);
                    move = getMove(p);
                    if (move != null) moves.Add(move);
                }
            }
            return moves;
        }
    }
}
