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
    class Level_1
    {
        //Player
        public Player player;
        private int StarUpTimer;
        public bool IsLevelStartup = true;

        Fonts LevelTitle;

        //Enemy
        public List<Enemy> enemy = new List<Enemy>();

        //HUD
        public HUD HeadsUpDisplay;

        //misc
        public Vector2 screensize_pos;
        public Rectangle screensize_rect;
        public ContentManager Content;
        private Game1 game1;
        public bool IsReset = false;
        private int ResetHealth;

        //background
        public Level_Background background = new Level_Background();

        //collision check
        public CollisionCheck collision = new CollisionCheck();

        //Check the gametime
        GameTick gameTicks = new GameTick();
        private int GT;

        private int[] SpawnTimer = new int[5];


        //enemy spawner varibles
        private float AboveScreen = -100;
        private float BelowScreen = 800;




        public Level_1(ContentManager load_Content, Vector2 load_screensize_pos, Rectangle load_screensize_rect, Player load_player, int load_health)
        {
            //misc
            screensize_pos = load_screensize_pos;
            screensize_rect = load_screensize_rect;
            Content = load_Content;
            player = load_player;
            ResetHealth = load_health;


            //HUD
            HeadsUpDisplay = new HUD(load_Content, screensize_pos, screensize_rect);
            HeadsUpDisplay.Health = load_health;

            //Startup
            player.player.sprite_rect.Width = 32;
            player.player.sprite_rect.Height = 27;

            //background
            background.LoadContent(Content, screensize_pos, screensize_rect);
            background.scroll_speed = 1f;
            //setupslides
            background.SlideAdd("graphics/backgrounds/level1/level1_start",
                screensize_pos.Y / 2);
            background.SlideAdd("graphics/backgrounds/level1/level1_1",
                -1050);
            background.SlideAdd("graphics/backgrounds/level1/level1_2",
                -2150);

            //fonts
            LevelTitle = new Fonts(load_Content, screensize_pos.X / 2, screensize_pos.Y / 2 - 200);

        }
 
        public void Update(GameMode load_gamemode, Game1 load_game)
        {
            game1 = load_game;
            HeadsUpDisplay.Score = load_game.UniScore;

            if (IsLevelStartup)
                LevelStartup();

            if (IsReset)
                LevelReset();

            gameTicks.Update();

            HeadsUpDisplay.debug_GameTime = gameTicks.Ticks;

            GT = gameTicks.Ticks;

            background.Update();

            EnemyLayout();


            if (HeadsUpDisplay.Health <= 0)
            {
                IsReset = true;
                game1.UniScore = game1.PrevScore;
                HeadsUpDisplay.Health = ResetHealth;
            }

            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].Update(Content, load_game, player);

                //Damage Player and Enemy if Collision
                if (collision.checkcollision(player.player.sprite_colbox, enemy[i].sprite.sprite_colbox))
                {
                    HeadsUpDisplay.Health -= 50;

                    enemy[i].isAlive = false;
                    enemy[i].sprite.sprite_colbox = Rectangle.Empty;
                }

                //Damage Player and Set Enemy Bullet to false
                for (int j = 0; j < enemy[i].Bullet_list.Count; j++)
                {
                    if (collision.checkcollision(player.player.sprite_colbox, enemy[i].Bullet_list[j].bullet_tex.sprite_colbox))
                    {
                        enemy[i].Bullet_list[j].isAlive = false;
                        enemy[i].Bullet_list[j].bullet_tex.sprite_colbox = Rectangle.Empty;

                        HeadsUpDisplay.Health -= enemy[i].Bullet_list[j].Damage;
                    }
                }

                //Damage Enemy and Set Player Bullet to false
                for (int o = 0; o < player.BulletList.Count; o++)
                {

                    if (collision.checkcollision(player.BulletList[o].bullet_tex.sprite_colbox,
                        enemy[i].sprite.sprite_colbox))
                    {
                        player.BulletList[o].isAlive = false;
                        player.BulletList[o].bullet_tex.sprite_colbox = Rectangle.Empty;

                        player.BulletList[o].isHit = true;

                        enemy[i].Health -= player.BulletList[o].Damage;

                        if (enemy[i].Health <= 0)
                        {
                            enemy[i].isAlive = false;
                            enemy[i].sprite.sprite_colbox = Rectangle.Empty;
                        }
                    }
                }
            }

            if (load_gamemode.Mode == GameMode.EGameMode.PLAY)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    player.ControlsEnabled = false;
                    IsReset = true;
                    game1.UniScore = game1.PrevScore;
                    load_gamemode.Mode = GameMode.EGameMode.LEVELSELECT;
                }
            }
        }

        public void LevelReset()
        {
            gameTicks.Ticks = 0;

            //Player
            player.player.sprite_rect.Width = 32;
            player.player.sprite_rect.Height = 27;
            player.player.sprite_pos.X = screensize_pos.X / 2;
            player.player.sprite_pos.Y = screensize_pos.Y - 50;
            IsLevelStartup = true;

            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].isResetRemoved = true;
                enemy[i].isAlive = false;
                enemy[i].sprite.sprite_colbox = Rectangle.Empty;

                for (int j = 0; j < enemy[i].Bullet_list.Count; j++)
                {
                    enemy[i].Bullet_list[j].isAlive = false;
                    enemy[i].Bullet_list[j].bullet_tex.sprite_colbox = Rectangle.Empty;
                }
            }

            for (int i = 0; i < player.BulletList.Count; i++)
            {
                player.BulletList[i].BulletHitEffect.isDrawing = false;
                player.BulletList[i].isAlive = false;
                player.BulletList[i].bullet_tex.sprite_colbox = Rectangle.Empty;
            }

            background.Slides[0].sprite_pos.Y = screensize_pos.Y / 2;
            background.Slides[1].sprite_pos.Y = -1050;
            background.Slides[2].sprite_pos.Y = -2150;

            IsReset = false;
        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            //draw background first
            background.Draw(load_spriteBatch);


            //Enemy Draw
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].Draw(load_spriteBatch);
            }

            //Player draw
            player.Player_Draw(load_spriteBatch);


            if (IsLevelStartup)
            {
                LevelTitle.Draw_Large_Font(load_spriteBatch, "-First Strike-", 1f, Color.Black); 
            }


            // always draw HUD on top
            HeadsUpDisplay.Draw(load_spriteBatch);
        }




        //---------------------------------------------------------------//
        //--------------------------//Level Intro//----------------------//
        //---------------------------------------------------------------//

        public void LevelStartup()
        {
            player.ControlsEnabled = false;

            //plane take off
            StarUpTimer++;
            if (StarUpTimer >= 5)
            {
                player.player.sprite_rect.Width++;
                player.player.sprite_rect.Height++;
                StarUpTimer = 0;
            }

            if (player.player.sprite_rect.Height >= 55 && player.player.sprite_rect.Width >= 64)
            {
                player.player.sprite_rect.Width = 64;
                player.player.sprite_rect.Height = 55;
                StarUpTimer = 0;
                player.ControlsEnabled = true;
                IsLevelStartup = false;
            }
            else
                player.player.sprite_pos.Y -= 1;

        }

        //---------------------------------------------------------------//
        //--------------------------//Enemy Types//----------------------//
        //---------------------------------------------------------------//

        public void EnemySpawnBasic(float Xpos, float Xdir, bool Canshoot)
        {
            Enemy BasicEnemy = new Enemy();

            Vector2 pos = new Vector2(Xpos, AboveScreen);
            Vector2 dir = new Vector2(Xdir, BelowScreen);
            Vector2 dirtoscreen = dir - pos;
            dirtoscreen.Normalize();

            BasicEnemy.Texture = "graphics/gameplay/enemies/biplane_e";
            BasicEnemy.position = pos;
            BasicEnemy.direction = dirtoscreen;
            BasicEnemy.MaxHealth = 25;
            BasicEnemy.Health = BasicEnemy.MaxHealth;
            BasicEnemy.score = 50;
            BasicEnemy.speed = 2f;
            BasicEnemy.Width = 48;
            BasicEnemy.Height = 37;
            BasicEnemy.EnemyHasWeapon = Canshoot;
            BasicEnemy.WeaponFireDir = new Vector2(player.player.sprite_pos.X, 800);
            BasicEnemy.WeaponFireRate = 150;
            BasicEnemy.BulletSpeed = 5f;
            BasicEnemy.BulletDamage = 5;

            enemy.Add(BasicEnemy);
        }

        public void EnemySpawnAdvanced(Vector2 pos, Vector2 dir, float speed, int health, int score)
        {

            Enemy AdvancedEnemy = new Enemy();

            Vector2 dirtoscreen = dir - pos;
            dirtoscreen.Normalize();

            AdvancedEnemy.Texture = "graphics/gameplay/enemies/biplane_ea";
            AdvancedEnemy.position = pos;
            AdvancedEnemy.direction = dirtoscreen;
            AdvancedEnemy.MaxHealth = health;
            AdvancedEnemy.Health = AdvancedEnemy.MaxHealth;
            AdvancedEnemy.score = score;
            AdvancedEnemy.speed = speed;
            AdvancedEnemy.Width = 64;
            AdvancedEnemy.Height = 55;


            enemy.Add(AdvancedEnemy);
        }


        //---------------------------------------------------------------//
        //--------------------------//Enemy Layout//---------------------//
        //---------------------------------------------------------------//

        public void EnemyLayout()
        {
            //Basic Wave Middle Down
            if (GT >= 5 && GT <= 10 || GT >= 23 && GT <= 26)
            {
                SpawnTimer[0]++;
                if (SpawnTimer[0] >= 50)
                {
                    EnemySpawnBasic(screensize_pos.X / 2, screensize_pos.X / 2, false);
                    SpawnTimer[0] = 0;
                }
            }
            //Basic Wave Left Down
            if (GT >= 8 && GT <= 12)
            {
                SpawnTimer[1]++;
                if (SpawnTimer[1] >= 50)
                {
                    EnemySpawnBasic(screensize_pos.X / 4, screensize_pos.X / 4, false);
                    SpawnTimer[1] = 0;
                }
            }
            //Basic Wave Right Down
            if (GT >= 10 && GT <= 15)
            {
                SpawnTimer[2]++;
                if (SpawnTimer[2] >= 50)
                {
                    EnemySpawnBasic(screensize_pos.X / 4 * 3, screensize_pos.X / 4 * 3, false);
                    SpawnTimer[2] = 0;
                }
            }
            //Basic Wave Left of screen to Right
            if (GT >= 18 && GT <= 21)
            {
                SpawnTimer[3]++;
                if (SpawnTimer[3] >= 50)
                {
                    EnemySpawnBasic(screensize_pos.X / 4, screensize_pos.X / 4 * 3, true);
                    SpawnTimer[3] = 0;
                }
            }
            //Basic Wave Right of screen to Left
            if (GT >= 20 && GT <= 26)
            {
                SpawnTimer[4]++;
                if (SpawnTimer[4] >= 50)
                {
                    EnemySpawnBasic(screensize_pos.X / 4 * 3, screensize_pos.X / 4, true);
                    SpawnTimer[4] = 0;
                }
            }
        }
    }
}
