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
    class Fonts
    {
        //fonts
        private SpriteFont font_score;
        private SpriteFont font_title;
        private SpriteFont font_debug;
        public Vector2 font_pos;

        public Fonts(ContentManager load_Content, float x, float y)
        {
            //fonts 
            font_score = load_Content.Load<SpriteFont>("fonts/font_score");
            font_title = load_Content.Load<SpriteFont>("fonts/font_title");
            font_debug = load_Content.Load<SpriteFont>("fonts/font_debug");

            font_pos.X = x;
            font_pos.Y = y;
        }

        public void Set_Pos(float x, float y)
        {
            font_pos.X = x;
            font_pos.Y = y;
        }

        public void Set_X(float x)
        {
            font_pos.X = x;
        }
        public void Set_Y(float y)
        {
            font_pos.Y = y;
        }

        public void Draw_Large_Font(SpriteBatch load_spriteBatch, string font_text, float font_size, Color color)
        {
            Vector2 fontsize = new Vector2();
            fontsize = font_title.MeasureString(font_text);
            load_spriteBatch.DrawString(font_title, font_text, font_pos, color, 0,
                fontsize / 2,
                font_size,
                SpriteEffects.None,
                0.0f);
        }
        public void Draw_Medium_Font(SpriteBatch load_spriteBatch, string font_text, float font_size, Color color)
        {
            Vector2 fontsize = new Vector2();
            fontsize = font_score.MeasureString(font_text);
            load_spriteBatch.DrawString(font_score, font_text, font_pos, color, 0,
                fontsize / 2,
                font_size,
                SpriteEffects.None,
                0.0f);
        }
        public void Draw_Small_Font(SpriteBatch load_spriteBatch, string font_text, float font_size, Color color)
        {
            Vector2 fontsize = new Vector2();
            fontsize = font_debug.MeasureString(font_text);
            load_spriteBatch.DrawString(font_debug, font_text, font_pos, color, 0,
                fontsize / 2,
                font_size,
                SpriteEffects.None,
                0.0f);
        }

    }
}
