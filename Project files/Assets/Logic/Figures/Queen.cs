using System.Collections.Generic;
using System.Linq;

namespace Szachy
{
    class Queen : Figure
    {
        internal Queen(Color color, Position position) 
            : base(color, position)
        {
        }

        public override List<Move> calculateMoves()
        {
            Bishop b = new Bishop(Color, Position);
            Rook r = new Rook(Color, Position);

            List<Move> moves = b.calculateMoves().Union(r.calculateMoves()).ToList();
            moves.ForEach(m => m.Figure = this);
            return moves;
        }
    }
}
