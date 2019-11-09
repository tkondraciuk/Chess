using System.Linq;
using UnityEngine;

namespace Szachy
{
    class Castling : Move
    {
        Rook rook;
        Position rookDestination;
        internal Rook Rook
        {
            get
            {
                return rook;
            }

            set
            {
                rook = value;
            }
        }

        public Position RookDestination
        {
            get
            {
                return rookDestination;
            }

            set
            {
                rookDestination = value;
            }
        }

        public Castling(Position destination, Figure figure, Rook rook, Position rookDestination) 
            : base(destination, figure)
        {
            this.Rook = rook;
            this.RookDestination = rookDestination;
        }

        public override void ReadMovement()
        {
            Client client = GameObject.FindObjectOfType<Client>();
            Position rookPosition = rook.Position;
            Position fromPosition = Figure.Position;
            Debug.Log("Poruszono wieżę z pozycji: " + rookPosition + " na pozycję: " + rookDestination);
            Debug.Log("Poruszono króla z pozycji: " + fromPosition + " na pozycję: " + Destination);
            client.Send("CCMOV|" + fromPosition + "|" + Destination + "|" + rookPosition + "|" + rookDestination);
        }

        public override void ExecuteMovement()
        {
            rook.setPosition(rookDestination);
            FigureController fc = GameObject.FindGameObjectsWithTag("Figure")
                .Select(f => f.GetComponent<FigureController>())
                .Where(c => c.Figure.Equals(Rook))
                .FirstOrDefault();
            fc.SetTargetPosition(RookDestination);

            Figure.PossibleMoves.Clear();
            base.ExecuteMovement();
        }
    }
}
