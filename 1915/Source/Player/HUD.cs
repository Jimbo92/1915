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
    class HUD
    {
        //HUD
        private Sprite HUDspr = new Sprite();
        private Sprite BAmmo_Gauge = new Sprite();
        private Sprite HP_Counter = new Sprite();
        private Sprite Point_Counter = new Sprite();

        //HUD Fonts
        private Fonts HP_Font;
        private Fonts Point_Font;

        public int Score;
        public int Health;

        public int debug_GameTime;
        private Fonts GT_Font;



        public HUD(ContentManager load_content, Vector2 load_screensize_pos, Rectangle load_screensize_rect)
        {
            //HUD
            HUDspr.SpriteLoadContent("graphics/gameplay/ui/gameui", load_content,
                load_screensize_pos.X / 2, load_screensize_pos.Y / 2, load_screensize_rect.Width, load_screensize_rect.Height);
            //health gauge
            HP_Counter.SpriteLoadContent("graphics/gameplay/ui/counter", load_content,
                30, load_screensize_pos.Y - 70, 56, 32);
            //ammo gauge
            //BAmmo_Gauge.SpriteLoadContent("graphics/gameplay/ui/gauge2", load_content,
            //    75, load_screensize_pos.Y - 65, 32, 32);
            //weapon gauge
            Point_Counter.SpriteLoadContent("graphics/gameplay/ui/counter", load_content,
                50, load_screensize_pos.Y - 25, 96, 48);

            //fonts
            HP_Font = new Fonts(load_content, HP_Counter.sprite_pos.X, HP_Counter.sprite_pos.Y);
            Point_Font = new Fonts(load_content, Point_Counter.sprite_pos.X, Point_Counter.sprite_pos.Y);

            GT_Font = new Fonts(load_content, 100, 50);
        }

        public void Update()
        {
            

        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            //always draw HUD on top
            HUDspr.SpriteDrawContent(load_spriteBatch);
            HP_Counter.SpriteDrawContent(load_spriteBatch);
            //BAmmo_Gauge.SpriteDrawContent(load_spriteBatch);
            Point_Counter.SpriteDrawContent(load_spriteBatch);

            //fonts
            HP_Font.Draw_Medium_Font(load_spriteBatch, Health.ToString(), 0.5f, Color.Black);
            Point_Font.Draw_Medium_Font(load_spriteBatch, Score.ToString(), 0.5f, Color.Black);

            GT_Font.Draw_Small_Font(load_spriteBatch, "GT: " + debug_GameTime.ToString(), 1, Color.Red);
        }

    }
}
