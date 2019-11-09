using System.Linq;
using UnityEngine;

namespace Szachy
{
    class Attack : Move
    {
        Figure target;
        internal Figure Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }


        public Attack(Position destination, Figure figure, Figure target) 
            : base(destination, figure)
        {
            this.Target = target;

        }

        public override void ReadMovement()
        {
            Client client = GameObject.FindObjectOfType<Client>();
            Position fromPosition = Figure.Position;
            Debug.Log("Poruszono figurę z pozycji: " + fromPosition + " na pozycję: " + Destination);
            client.Send("CAMOV|" + fromPosition + "|" + Destination);
        }

        public override void ExecuteMovement()
        {
            GameObject target = GameObject.FindGameObjectsWithTag("Figure")
                .FirstOrDefault(g => Position.GetTransform(Destination).Equals(g.transform.position));
            Object.Destroy(target);
            base.ExecuteMovement();
            Board.RemoveFigure(Target);
        }
    }
}
