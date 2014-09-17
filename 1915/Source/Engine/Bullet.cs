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
    class Bullet
    {
        public Vector2 bullet_pos;
        public Vector2 bullet_dir;
        public float bullet_speed;
        public Sprite bullet_tex;
        private ContentManager Content;
        public bool isAlive = true;
        public int Damage;

        //Effects
        public Effects BulletHitEffect = new Effects();

        public bool isHit;
        private int hitTimer;

        public void Update(ContentManager load_content, string load_texture, int w, int h)
        {
            //Effects
            BulletHitEffect.LoadContent(load_content, "graphics/gameplay/projectiles/BasicExplosionSS",
                bullet_pos.X, bullet_pos.Y, 8, 7);

            BulletHitEffect.Update();

            if (isHit)
            {
                BulletHitEffect.isDrawing = true;
                hitTimer++;
            }

            //if (hitTimer >= 5 && isHit)
            //{
            //    BulletHitEffect.isDrawing = false;
            //}


            if (isAlive)
            {
                Content = load_content;

                bullet_tex = new Sprite();

                bullet_tex.SpriteLoadContent(load_texture, Content, bullet_pos.X, bullet_pos.Y, w, h);

                bullet_tex.SpriteSetColbox2();

                bullet_pos += bullet_dir * bullet_speed;
            }

        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            if (isAlive)
            {
                bullet_tex.SpriteDrawContent(load_spriteBatch);
                //bullet_tex.SpriteDrawDebug(load_spriteBatch);
            }

            BulletHitEffect.Draw(load_spriteBatch);

        }
    }
}
