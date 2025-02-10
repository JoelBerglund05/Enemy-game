using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyMonoGame
{
    internal interface IActor
    {
        public Vector2 Position { get; set; }
        public int Money { get; set; }
    }
}
