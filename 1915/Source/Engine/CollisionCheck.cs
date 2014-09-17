using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using System.Text;

namespace _1915
{
    class CollisionCheck
    {
        //collision check
        public bool checkcollision(Rectangle a, Rectangle b)
        {
            if (a.X + a.Width < b.X || a.Y + a.Height < b.Y || a.X > b.X + b.Width || a.Y > b.Y + b.Height)
            {
                return false;
            }

            return true;
        }
    }
}
