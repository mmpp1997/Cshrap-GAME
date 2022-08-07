using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Letjelica : Sprite
    {
        public Letjelica(string put, int x, int y)
            : base(put, x, y)
        {
            
        }
        protected int brzina;

        public int Brzina
        {
            get { return brzina; }
            set { brzina = value; }
        }
        protected int bodovi;

        public int Bodovi
        {
            get { return bodovi; }
            set { bodovi = value; }
        }
        protected int zivot;

        public int Zivot
        {
            get { return zivot; }
            set { zivot = value; }
        }


        public override int X
        {
            get { return x; }
            set
            {
                if (value + this.Width > GameOptions.RightEdge || value < GameOptions.LeftEdge)
                {

                }
                else
                    x = value;
            }
        }
        public override int Y
        {
            get { return y; }
            set
            {
                if (value + this.Heigth+125 > GameOptions.DownEdge|| value < GameOptions.UpEdge)
                {

                }
                else
                    y = value;
            }
        }
    }
    public class Ufo : Letjelica
    {
        public Ufo(string put, int x, int y)
            : base(put, x, y)
        {
            Bodovi = 0;
            Brzina = 12;
            Zivot = 2;
        }
    }
    public class Plane : Letjelica
    {
        public Plane(string put, int x, int y)
            : base(put, x, y)
        {
            Bodovi = 0;
            Brzina = 10;
            Zivot = 4;
        }
    }
}
