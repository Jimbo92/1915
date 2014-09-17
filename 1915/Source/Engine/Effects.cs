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
    class Effects
    {
        public SpriteAnim spriteAnim;
        public Vector2 position;

        public Texture2D Texture;

        public bool isDrawing = false;

        public void LoadContent(ContentManager load_content, string load_texture, float x, float y, int load_rows, int load_columns)
        {
            Texture = load_content.Load<Texture2D>(load_texture);

            position.X = x;
            position.Y = y;
            spriteAnim = new SpriteAnim(Texture, load_rows, load_columns);
        }
        public void Update()
        {
            if (isDrawing)
            {
                spriteAnim.Update();
            }

        }
        public void Draw(SpriteBatch load_spriteBatch)
        {
            if (isDrawing)
            {
                spriteAnim.Draw(load_spriteBatch, new Vector2(position.X, position.Y));
            }
        }



    }
}
