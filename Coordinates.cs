using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public readonly struct Coordinates
    {
        public int X{ get; }
        public int Y{ get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
