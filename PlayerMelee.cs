﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EasyMonoGame;

namespace EasyStart
{
    internal class PlayerMelee : Melee
    {
        public PlayerMelee(IActor player) : base(player)
        {
        }
    }
}
