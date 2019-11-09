using System;
using System.Collections.Generic;
using UnityEngine;

namespace Szachy
{
    public class Position
    {
        static List<char> columnTags = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        int x;
        int y;
        

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }
        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
        public char Column
        {
            get { return columnTags[X]; }
            set { X = columnTags.IndexOf(Char.ToUpper(value)); }
        }
        public int Row
        {
            get { return Y + 1; }
            set { Y = value - 1; }
        }

        public Position this[int xTransform, int yTransform]
        {
           get { return new Position(X + xTransform, Y + yTransform); }
        }


        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public Position(Position p) 
            : this(p.X, p.Y) { }

        public static Position[] GetPositionsBetween(Position p1, Position p2)
        {
            List<Position> list = new List<Position>();
            int stepX = Math.Sign(p2.X - p1.X);
            int stepY = Math.Sign(p2.Y - p1.Y);

            for (Position p = new Position(p1); p.Equals(p2); p = p[stepX, stepY])
                list.Add(p);

            return list.ToArray();
        }

        public double getDistance(Position p)
        {
            return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(X - p.X, 2));
        }

        public static Vector3 GetTransform(Position position)
        {


            return MainController.UnityCords[position];
        }

        public override string ToString()
        {
            return Column.ToString() + Row;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position)) return false;
            Position p = obj as Position;
            return p.X == X && p.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public bool isValid()
        {
            return X >= 0 && Y >= 0 && X < 8 && Y < 8;
        }

        public static implicit operator string(Position p)
        {
            return p.ToString();
        }
        public static implicit operator Position(string s)
        {
            char c = s[0];
            int r = int.Parse(s[1].ToString());
            return new Position(c, r);
        }


    }
}
