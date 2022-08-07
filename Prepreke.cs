using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public abstract class Prepreke:Sprite
    {
        public Prepreke(string put,int x,int y)
            :base(put,x,y)
        {

        }
    }
    public class Zgrada : Prepreke
    {
        public Zgrada(string put,int x,int y)
            : base(put, x, y)
        {

        }
    }
    public class PowerUp : Prepreke
    {
        public PowerUp(string put, int x, int y)
            : base(put, x, y)
        {

        }
    }
}
