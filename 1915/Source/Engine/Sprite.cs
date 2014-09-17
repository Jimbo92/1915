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
    class Sprite
    {
        public Texture2D sprite;
        public Texture2D sprite_debug;
        public Rectangle sprite_colbox = new Rectangle();
        public Vector2 sprite_pos = new Vector2();
        public Rectangle sprite_rect = new Rectangle();

        public Rectangle source_rect = new Rectangle();

        private bool ispixel = false;

        public void SpriteLoadPixel(GraphicsDevice graphics, ContentManager content, float x, float y, int w, int h, Color color)
        {
            sprite_debug = content.Load<Texture2D>("graphics/collisionbox");
            sprite = new Texture2D(graphics, w, h);

            Color[] Sdata = new Color[w * h];
            for (int i = 0; i < Sdata.Length; i++)
            {
                Sdata[i] = color;
            }
            sprite.SetData(Sdata);

            sprite_pos.X = x;
            sprite_pos.Y = y;

            sprite_rect.X = (int)sprite_pos.X - sprite.Width / 2;
            sprite_rect.Y = (int)sprite_pos.Y - sprite.Height / 2;

            sprite_rect.Width = w;
            sprite_rect.Height = h;

            sprite_colbox.Width = w;
            sprite_colbox.Height = h;

            ispixel = true;

        }
        public void SpriteLoadContent(string FileSource, ContentManager content, float x, float y, int w, int h)
        {

            sprite_debug = content.Load<Texture2D>("graphics/collisionbox");
            sprite = content.Load<Texture2D>(FileSource);

            sprite_pos.X = x;
            sprite_pos.Y = y;

            sprite_rect.X = (int)sprite_pos.X;
            sprite_rect.Y = (int)sprite_pos.Y;

            sprite_rect.Width = w;
            sprite_rect.Height = h;

            sprite_colbox.Width = sprite_rect.Width;
            sprite_colbox.Height = sprite_rect.Height;

            ispixel = false;


        }


        public void SpriteSetPos(float x, float y)
        {
            sprite_pos.X = x;
            sprite_pos.Y = y;
            sprite_rect.X = (int)sprite_pos.X;
            sprite_rect.Y = (int)sprite_pos.Y;
        }
        public void SpriteSetColbox()
        {
            if (ispixel != false)
            {
                sprite_colbox.X = (int)sprite_pos.X - sprite.Width / 2;
                sprite_colbox.Y = (int)sprite_pos.Y - sprite.Height / 2;
                sprite_rect.X = (int)sprite_pos.X - sprite.Width / 2;
                sprite_rect.Y = (int)sprite_pos.Y - sprite.Height / 2;
            }
            else
            {
                sprite_colbox.X = (int)sprite_pos.X - sprite_rect.Width / 2;
                sprite_colbox.Y = (int)sprite_pos.Y - sprite_rect.Height / 2;
                sprite_rect.X = (int)sprite_pos.X - sprite.Width / 2;
                sprite_rect.Y = (int)sprite_pos.Y - sprite.Height / 2;
            }
        }
        public void SpriteSetColbox2()
        {

            sprite_rect.X = (int)sprite_pos.X;
            sprite_rect.Y = (int)sprite_pos.Y;
            sprite_colbox.X = (int)sprite_pos.X - sprite_rect.Width / 2;
            sprite_colbox.Y = (int)sprite_pos.Y - sprite_rect.Height / 2;

        }
        public void SpriteSetColbox3()
        {
            sprite_rect.X = (int)sprite_pos.X;
            sprite_rect.Y = (int)sprite_pos.Y;
        }
        public void SpriteSetPos_X(float x)
        {
            sprite_pos.X = x;
        }
        public void SpriteSetPos_Y(float y)
        {
            sprite_pos.Y = y;
        }
        public void SetSpriteRect_W(int w)
        {
            sprite_rect.Width = w;
        }
        public void SetSpriteRect_H(int h)
        {
            sprite_rect.Height = h;
        }
        public void SpriteDrawPixel(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Rectangle((int)sprite_pos.X, (int)sprite_pos.Y, sprite.Width, sprite.Height),
                null,
                Color.White,
                0,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0);
        }
        public void SpriteDrawContent(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                sprite_rect,
                null,
                Color.White,
                0,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0);

        }
        public void SpriteDrawContent2(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Rectangle((int)sprite_pos.X, (int)sprite_pos.Y, sprite.Width, sprite.Height),
                null,
                Color.White,
                0,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0);
        }
        public void SpriteDrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Rectangle((int)sprite_pos.X, (int)sprite_pos.Y, sprite.Width, sprite.Height),
                source_rect,
                Color.White,
                0,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0);
        }
        public void SpriteDrawDebug(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite_debug, sprite_colbox, Color.White);
            spriteBatch.Draw(sprite_debug, sprite_rect, Color.LightGreen);
        }
        public void SpriteDispose()
        {
            sprite.Dispose();
        }

    }
}
