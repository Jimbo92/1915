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
    class Level_Background
    {
        //setup background slides
        public List<Sprite> Slides = new List<Sprite>();

        public Vector2 position;

        private ContentManager Content;
        private Vector2 screensize_pos;
        private Rectangle screensize_rect;

        public float scroll_speed;

        public bool isDrawing = true;

        public void LoadContent(ContentManager load_content, Vector2 load_screensize_pos, Rectangle load_screensize_rect)
        {
            Content = load_content;
            screensize_pos = load_screensize_pos;
            screensize_rect = load_screensize_rect;
        }

        public void Update()
        {
            //position.Y += scroll_speed;

            for (int i = 0; i < Slides.Count; i++)
            {
                Slides[i].SpriteSetColbox2();
                Slides[i].sprite_pos.Y += scroll_speed;
                 
            }
        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            if (isDrawing)
            {
                for (int i = 0; i < Slides.Count; i++)
                {
                    Slides[i].SpriteDrawContent(load_spriteBatch);
                }
            }
        }



        //--------------------------------------------------------//
        //-----------------------//SLIDES//-----------------------//
        //--------------------------------------------------------//

        public void SlideAdd(string load_Texture2D, float pos_y)
        {
            Sprite slide = new Sprite();

            position.Y = pos_y;

            slide.SpriteLoadContent(load_Texture2D, Content, screensize_pos.X / 2, position.Y,
                screensize_rect.Width, screensize_rect.Height * 2);

            Slides.Add(slide);

        }

    }
}
