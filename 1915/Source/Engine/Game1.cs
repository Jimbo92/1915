#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace _1915
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Misc
        Input input = new Input();

        //Player
        Player player;

        //Score & Health
        public int UniScore = 0;
        public int PrevScore;
        public bool PrevScoreUpdate = false;
        public int UniHealth = 30;

        //game mode
        GameMode gamemode = new GameMode();

        //mainmenu
        MainMenu Mmenu;

        //Level Select
        Level_Select LevelSelect = new Level_Select();

        //levels
        Level_1 level_1;

        //screensize
        Vector2 screensize;
        Rectangle screensize_rect;


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 700;
            graphics.ApplyChanges();

            graphics.IsFullScreen = false;

            graphics.ApplyChanges();

            screensize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            screensize_rect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);



            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content, screensize);

            Mmenu = new MainMenu(Content, screensize, screensize_rect, graphics, player);

            level_1 = new Level_1(Content, screensize, screensize_rect, player, UniHealth);

            LevelSelect.LoadContent(Content, screensize, gamemode, player);
        }

        protected override void Update(GameTime gameTime)
        {
            input.Begin();
            //Code goes below this

            if (input.KeyboardPressed(Keys.F12))
                UniScore += 1000;

            player.Player_Update(input);

            switch (gamemode.Mode)
            {
                case GameMode.EGameMode.MENU: Mmenu.Update(gamemode, this, input); break;
                case GameMode.EGameMode.LEVELSELECT: 
                    {
                        LevelSelect.Update(this, input);
                        PrevScoreUpdate = true;            
                    }; break;
                case GameMode.EGameMode.PLAY: 
                    {
                        level_1.Update(gamemode, this);
                        PrevScoreUpdate = false;
                    }; break;
            }

            

            if (PrevScoreUpdate)
                PrevScore = UniScore;

            if (gamemode.Mode == GameMode.EGameMode.PLAY)
                IsMouseVisible = false;
            else
                IsMouseVisible = true;


            //Code ends here
            input.End();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();

            switch (gamemode.Mode)
            {
                case GameMode.EGameMode.MENU: Mmenu.Draw(spriteBatch); break;
                case GameMode.EGameMode.LEVELSELECT: LevelSelect.Draw(spriteBatch); break;
                case GameMode.EGameMode.PLAY: level_1.Draw(spriteBatch); break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {

        }
    }
}
