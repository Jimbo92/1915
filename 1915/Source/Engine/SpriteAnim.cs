using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace _1915
{
    class SpriteAnim
    {
        public Texture2D Texture;
        public int Rows;
        public int Columns;
        private int currentFrame;
        private int totalFrames;
        public int width;
        public int height;

        public int AnimTimer;

        public SpriteAnim(Texture2D load_texture, int load_rows, int load_columns)
        {
            Texture = load_texture;
            Rows = load_rows;
            Columns = load_columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
        
        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch load_spriteBatch, Vector2 load_location)
        {
            width = Texture.Width / Columns;
            height = Texture.Height / Rows;

            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)load_location.X, (int)load_location.Y, width, height);

            load_spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
