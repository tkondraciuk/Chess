using System;
using System.Linq;

namespace Szachy
{

    class Player
    {
        const int TASK_COMPLETED = 0;
        const int EMPTY_FIELD_ERROR = 1;
        const int MOVEMENT_DURING_OPPONENT_TURN_ERROR = 2;
        const int CHECK_ERROR = 3;
        const int IMPOSSIBLE_MOVEMENT = 4;

        Color color;
        Figure selectedFigure;
        King king;

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
        internal Figure SelectedFigure
        {
            get
            {
                return selectedFigure;
            }

            set
            {
                selectedFigure = value;
            }
        }
        internal King King
        {
            get
            {
                return king;
            }

            set
            {
                king = value;
            }
        }

        public Player(Color color)
        {
            this.Color = color;
        }

        public int SelectFigure(Position position)
        {
            if (MainController.Turn != Color) return MOVEMENT_DURING_OPPONENT_TURN_ERROR; 
            SelectedFigure = MainController.Board.GetFigure(position);
            if (SelectedFigure == null || SelectedFigure.Color!=Color) return EMPTY_FIELD_ERROR;
            SelectedFigure.SavePossibleMoves();
            return TASK_COMPLETED;
        } 
        public int MoveFigure(Position position)
        {
            if (MainController.Turn != Color) return MOVEMENT_DURING_OPPONENT_TURN_ERROR;
            Move move = SelectedFigure.PossibleMoves
                .Where(m => m.Destination.Equals(position))
                .FirstOrDefault();

            if (move == null) return IMPOSSIBLE_MOVEMENT;
            move.ReadMovement();
            return TASK_COMPLETED;
        }
        

        public void setKing()
        {
            King = (King) MainController.Board.Figures
                .Where(f => f is King && f.Color == Color)
                .FirstOrDefault();
        }
    }
}
