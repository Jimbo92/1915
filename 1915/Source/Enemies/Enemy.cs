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
    class Enemy
    {
        public bool EnemyHasWeapon = false;
        public List<Bullet> Bullet_list = new List<Bullet>();
        private int BulletTimer;
        public int WeaponFireRate;
        public Vector2 WeaponFireDir;
        public float BulletSpeed;

        public Vector2 position;
        public Vector2 direction;
        public float speed;
        public Sprite sprite;
        public bool isAlive = true;
        public bool isResetRemoved = false;

        public int Width;
        public int Height;
        public string Texture;

        public int Health;
        public int MaxHealth;
        public int BulletDamage;

        public int score;
        private int scoreTimer;
        private bool settingScore = false;
        private bool setScore;

        private ContentManager Content;
        private Player player;

        public void Update(ContentManager load_Content, Game1 load_game, Player load_player)
        {
            player = load_player;

            if (EnemyHasWeapon && isAlive)
            {
                BasicEnemyBullet();
            }

            for (int i = 0; i < Bullet_list.Count; i++)
            {
                Bullet_list[i].Update(load_Content, "graphics/gameplay/projectiles/bullet_e", 5, 16);
            }

            if (isAlive && Health != 0)
            {
                Content = load_Content;

                sprite = new Sprite();

                sprite.SpriteLoadContent(Texture, Content, position.X, position.Y, Width, Height);

                sprite.SpriteSetColbox2();

                position += direction * speed;
            }
            else
                if (!isResetRemoved)
                settingScore = true;

            if (settingScore)
            {
                setScore = true;
                scoreTimer++;
            }

            if (scoreTimer >= 2 && settingScore)
            {
                setScore = false;
            }

            if (setScore)
            {
                load_game.UniScore += score;
            }

        }

        private void BasicEnemyBullet()
        {
            BulletTimer++;
            if (BulletTimer >= WeaponFireRate)
            {
                Bullet Bbullet = new Bullet();

                Bbullet.bullet_pos = position;

                Bbullet.bullet_dir = WeaponFireDir - Bbullet.bullet_pos;
                Bbullet.bullet_dir.Normalize();
                Bbullet.bullet_speed = BulletSpeed;
                Bbullet.Damage = BulletDamage;

                Bullet_list.Add(Bbullet);
                BulletTimer = 0;
            }
        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            //bullet draw
            for (int i = 0; i < Bullet_list.Count; i++)
            {
                Bullet_list[i].Draw(load_spriteBatch);
            }

            if (isAlive && Health != 0)
            {
                sprite.SpriteDrawContent(load_spriteBatch);
                //enemy_sprite.SpriteDrawDebug(load_spriteBatch);
            }
        }

    }
}
