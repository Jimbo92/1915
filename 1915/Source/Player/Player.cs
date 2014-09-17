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
    class Player
    {
        public enum EControls
        {
            Mouse,
            Keyboard
        };

        public enum EWeapons
        {
            Basic,
            Advanced,
            Extreme,
            Ultimate
        };

        //Misc
        Input input;

        public EControls controls = EControls.Mouse;
        public EWeapons weapons = EWeapons.Basic;

        //Player
        public Sprite player = new Sprite();
        private Vector2 Player_vel;
        private Vector2 Player_dir;
        private float Player_speed;
        private Vector2 Pgotopos;
        private Vector2 getmouse;
        public List<Bullet> BulletList = new List<Bullet>();
        private Vector2 screensize_pos;
        private ContentManager Content;

        public bool ControlsEnabled = false;

        //bullet & weapon
        private int timer;

        //collision
        CollisionCheck collision = new CollisionCheck();

        //aimer
        private Sprite Aim = new Sprite();


        public Sprite GetSprite()
        {
            return player;
        }

        public Player(ContentManager load_Content, Vector2 load_screensize_pos)
        {
            screensize_pos = load_screensize_pos;
            Content = load_Content;

            Pgotopos = new Vector2(load_screensize_pos.X / 2 + 1, load_screensize_pos.Y - 50);

            //Player
            player.SpriteLoadContent("graphics/gameplay/player/biplane", load_Content,
                load_screensize_pos.X / 2, load_screensize_pos.Y - 50, 64, 55);

            //aim load
            Aim.SpriteLoadContent("graphics/gameplay/player/aim", load_Content,
                Mouse.GetState().X,
                Mouse.GetState().Y,
                32, 32);
        }

        public void Player_Update(Input load_input)
        {
            //Misc
            input = load_input;


            if (ControlsEnabled)
            {
                switch (controls)
                {
                    case EControls.Mouse: MouseControl(); break;
                    case EControls.Keyboard: KeyboardControl(); break;
                }

                Weapon();
            }

            player.SpriteSetColbox2();

        }

        private void MouseControl()
        {

            Aim.SpriteSetPos(Mouse.GetState().X, Mouse.GetState().Y);

            getmouse.X = Mouse.GetState().X;
            getmouse.Y = Mouse.GetState().Y;

            player.SpriteSetColbox();

            Player_dir = getmouse - player.sprite_pos;
            Player_speed = Player_dir.Length() * 0.05f;
            Player_dir.Normalize();

            Player_vel = Player_dir * Player_speed;
            player.SpriteSetPos_X(player.sprite_pos.X + Player_vel.X);
            player.SpriteSetPos_Y(player.sprite_pos.Y + Player_vel.Y);

        }
        private void KeyboardControl()
        {

            if (input.KeyboardPress(Keys.Left))
                Pgotopos.X -= 6;
            if (input.KeyboardPress(Keys.Right))
                Pgotopos.X += 6;
            if (input.KeyboardPress(Keys.Up))
                Pgotopos.Y -= 3;
            if (input.KeyboardPress(Keys.Down))
                Pgotopos.Y += 5;

            player.SpriteSetColbox();

            Player_dir = Pgotopos - player.sprite_pos;
            Player_speed = Player_dir.Length() * 0.1f;
            Player_dir.Normalize();

            Player_vel = Player_dir * Player_speed;
            player.SpriteSetPos_X(player.sprite_pos.X + Player_vel.X);
            player.SpriteSetPos_Y(player.sprite_pos.Y + Player_vel.Y);
        }

        public void Player_Draw(SpriteBatch load_spriteBatch)
        {
            for (int i = 0; i < BulletList.Count; i++)
                BulletList[i].Draw(load_spriteBatch);

            player.SpriteDrawContent(load_spriteBatch);
            //player.SpriteDrawDebug(load_spriteBatch);

            if (controls == EControls.Mouse && ControlsEnabled)
                Aim.SpriteDrawContent(load_spriteBatch);
        }


        //---------------------------------------------------------------//
        //--------------------------//Weapon Code//----------------------//
        //---------------------------------------------------------------//

        private void Weapon()
        {

            //Keyboard controls
            if (controls == EControls.Keyboard)
            {
                if (input.KeyboardPressed(Keys.RightControl))
                    Shoot();

                if (input.KeyboardPress(Keys.RightControl))
                {
                    timer++;
                    if (timer == 13)
                    {
                        Shoot();
                        timer = 0;
                    }
                }
            }

            //Mouse controls
            if (controls == EControls.Mouse)
            {
                if (input.ClickPressed(Input.EClicks.LEFT))
                    Shoot();

                if (input.ClickPress(Input.EClicks.LEFT))
                {
                    timer++;
                    if (timer == 13)
                    {
                        Shoot();
                        timer = 0;
                    }
                }
            }

            for (int i = 0; i < BulletList.Count; i++)
            {

                BulletList[i].Update(Content, "graphics/gameplay/projectiles/bullet", 5, 32);

                
                if (BulletList[i].bullet_pos.Y <= -100 || BulletList[i].bullet_pos.Y >= screensize_pos.Y +100)
                {
                    BulletList[i].isAlive = false;
                    BulletList[i].bullet_tex.sprite_colbox = Rectangle.Empty;
                }
                 
                
            }
        }

        private void Shoot()
        {
            if (weapons == EWeapons.Basic)
            {
                shoot_basic(20);
            }
            if (weapons == EWeapons.Advanced)
            {
                shoot_basic(20);
                shoot_advanced(15);
            }
            if (weapons == EWeapons.Extreme)
            {
                shoot_basic(20);
                shoot_advanced(15);
                shoot_extreme(20);
            }
            if (weapons == EWeapons.Ultimate)
            {
                shoot_basic(20);
                shoot_advanced(15);
                shoot_extreme(20);
                shoot_ultimate(20);
            }
        }

        public void shoot_basic(float speed)
        {
            Bullet bullet = new Bullet();
            bullet.bullet_pos = new Vector2(player.sprite_pos.X, player.sprite_pos.Y);
            bullet.bullet_dir = new Vector2(player.sprite_pos.X, -100) - bullet.bullet_pos;
            bullet.bullet_dir.Normalize();
            bullet.bullet_speed = speed;
            bullet.Damage = 8;

            BulletList.Add(bullet);
        }
        public void shoot_advanced(float speed)
        {
            Bullet bullet2 = new Bullet();
            bullet2.bullet_pos = new Vector2(player.sprite_pos.X - 20, player.sprite_pos.Y - 30);
            bullet2.bullet_dir = new Vector2(player.sprite_pos.X - 20, -100) - bullet2.bullet_pos;
            bullet2.bullet_dir.Normalize();
            bullet2.bullet_speed = speed;
            bullet2.Damage = 4;

            Bullet bullet3 = new Bullet();
            bullet3.bullet_pos = new Vector2(player.sprite_pos.X + 20, player.sprite_pos.Y - 30);
            bullet3.bullet_dir = new Vector2(player.sprite_pos.X + 20, -100) - bullet3.bullet_pos;
            bullet3.bullet_dir.Normalize();
            bullet3.bullet_speed = speed;
            bullet3.Damage = 4;

            BulletList.Add(bullet2);
            BulletList.Add(bullet3);
        }
        public void shoot_extreme(float speed)
        {
            Bullet bullet4 = new Bullet();
            bullet4.bullet_pos = new Vector2(player.sprite_pos.X, player.sprite_pos.Y);
            bullet4.bullet_dir = new Vector2(player.sprite_pos.X + 100, -100) - bullet4.bullet_pos;
            bullet4.bullet_dir.Normalize();
            bullet4.bullet_speed = speed;
            bullet4.Damage = 2;

            Bullet bullet5 = new Bullet();
            bullet5.bullet_pos = new Vector2(player.sprite_pos.X, player.sprite_pos.Y);
            bullet5.bullet_dir = new Vector2(player.sprite_pos.X - 100, -100) - bullet5.bullet_pos;
            bullet5.bullet_dir.Normalize();
            bullet5.bullet_speed = speed;
            bullet5.Damage = 2;

            BulletList.Add(bullet4);
            BulletList.Add(bullet5);
        }
        public void shoot_ultimate(float speed)
        {
            Bullet bullet = new Bullet();
            bullet.bullet_pos = new Vector2(player.sprite_pos.X, player.sprite_pos.Y);
            bullet.bullet_dir = new Vector2(player.sprite_pos.X, 800) - bullet.bullet_pos;
            bullet.bullet_dir.Normalize();
            bullet.bullet_speed = speed;
            bullet.Damage = 8;

            BulletList.Add(bullet);
        }

    }
}
