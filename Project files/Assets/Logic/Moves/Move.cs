using System.IO;
using System.Linq;
using UnityEngine;

namespace Szachy
{
    class Move
    {
        private Position destination;
        private Figure figure;
        private Board board;
        private Position source;

        internal Position Destination { get { return destination; } set { destination = value; } }
        internal Figure Figure { get { return figure; }     set { figure = value; } }
        internal Board Board { get { return board; }    set { board = value; } }
        internal Position Source { get { return source; }   set { source = value; } }


        public Move(Position destination, Figure figure)
        {
            this.Destination = new Position(destination);
            this.Figure = figure;
            Board = MainController.Board;
        }

        public virtual void ReadMovement()
        {
            Client client = GameObject.FindObjectOfType<Client>();
            Position fromPosition = Figure.Position;
            Debug.Log("Poruszono figurę z pozycji: " + fromPosition + " na pozycję: " + Destination);
            client.Send("CMOV|" + fromPosition + "|" + Destination);
        }
        
        public virtual void ExecuteMovement()
        {
            Figure.setPosition(Destination);
            FigureController fc = GameObject.FindGameObjectsWithTag("Figure")
                .Select(f => f.GetComponent<FigureController>())
                .Where(c => c.Figure.Equals(Figure))
                .FirstOrDefault();
            fc.SetTargetPosition(Destination);
            Board.isCheck();
            Figure.PossibleMoves.Clear();
        }

        public override string ToString()
        {
            return Destination.ToString();
        }

    }
}
