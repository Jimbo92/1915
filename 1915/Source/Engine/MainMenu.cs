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
    class MainMenu
    {

        //Menu

        enum EMenuMode
        {
            Menu,
            Options,
            LevelSelect,
            Upgrades,
            Pause
        };

        //Misc
        Input input;

        SpriteAnim test;
        Texture2D testTex;

        //Main Menu buttons
        Button StartGameBtn = new Button();
        Button OptionsBtn = new Button();
        Button QuitBtn = new Button();

        //Options Buttons
        Button MouseControlsBtn = new Button();
        Button KeyboardControlsBtn = new Button();
        Button BackBtn = new Button();

        //background
        private Sprite mBackground = new Sprite();

        //fonts
        Fonts Font_Title;
        Fonts Font_Controls;


        private Vector2 screensize_pos;
        private Rectangle screensize_rect;

        GraphicsDeviceManager graphics;

        GameMode gamemode;

        EMenuMode menuMode = EMenuMode.Menu;

        Player player;

        Game game;


        public MainMenu(ContentManager load_Content, Vector2 load_screensize_pos, Rectangle load_screensize_rect, GraphicsDeviceManager load_graphics, Player load_player)
        {
            player = load_player;

            testTex = load_Content.Load<Texture2D>("graphics/gameplay/projectiles/BasicExplosionSS");

            test = new SpriteAnim(testTex, 8, 7);

            //Button load
            //startgame
            StartGameBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 256, 64);
            StartGameBtn.ButtonName = "Start Game";
            StartGameBtn.FontSize = 1f;
            StartGameBtn.FontColour = Color.Black;
            //options
            OptionsBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 196, 64);
            OptionsBtn.ButtonName = "Options";
            OptionsBtn.FontSize = 1f;
            OptionsBtn.FontColour = Color.Black;
            //quit
            QuitBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 128, 64);
            QuitBtn.ButtonName = "Quit";
            QuitBtn.FontSize = 1f;
            QuitBtn.FontColour = Color.Black;

            //options
            //controls
            MouseControlsBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 196, 64);
            MouseControlsBtn.ButtonName = "Mouse";
            MouseControlsBtn.FontSize = 1f;
            MouseControlsBtn.FontColour = Color.Black;

            KeyboardControlsBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 196, 64);
            KeyboardControlsBtn.ButtonName = "Keyboard";
            KeyboardControlsBtn.FontSize = 1f;
            KeyboardControlsBtn.FontColour = Color.Black;

            BackBtn.LoadContentOverlay(load_Content, "graphics/menu/menubutton", "graphics/menu/underline", 128, 64);
            BackBtn.ButtonName = "Back";
            BackBtn.FontSize = 1f;
            BackBtn.FontColour = Color.Black;


            screensize_pos = load_screensize_pos;
            screensize_rect = load_screensize_rect;
            graphics = load_graphics;

            //menu
            //background
            mBackground.SpriteLoadContent("graphics/menu/background",
                load_Content,
                screensize_pos.X / 2,
                screensize_pos.Y / 2,
                screensize_rect.Width,
                screensize_rect.Height);

            Font_Title = new Fonts(load_Content, screensize_pos.X / 2, screensize_pos.Y / 2 - 270);
            Font_Controls = new Fonts(load_Content, screensize_pos.X / 2, screensize_pos.Y / 2 - 150);

        }

        public void Update(GameMode load_gamemode, Game load_game, Input load_input)
        {
            gamemode = load_gamemode;
            game = load_game;
            input = load_input;

            test.Update();

            if (gamemode.Mode == GameMode.EGameMode.MENU)
            {

                switch (menuMode)
                {
                    case EMenuMode.Menu: Main(); break;
                    case EMenuMode.Options: Options(); break;
                }

            }

        }

        public void Main()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            //buttons
            //startgame
            StartGameBtn.Update(new Vector2(screensize_pos.X / 2, screensize_pos.Y / 2));
            if (StartGameBtn.collision.checkcollision(StartGameBtn.GetMousePos, StartGameBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    gamemode.Mode = GameMode.EGameMode.LEVELSELECT;

            //Options
            OptionsBtn.Update(new Vector2(screensize_pos.X / 2, screensize_pos.Y / 2 + 96));
            if (OptionsBtn.collision.checkcollision(OptionsBtn.GetMousePos, OptionsBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    menuMode = EMenuMode.Options;

            //Quit
            QuitBtn.Update(new Vector2(screensize_pos.X / 2, screensize_pos.Y / 2 + 192));
            if (QuitBtn.collision.checkcollision(QuitBtn.GetMousePos, QuitBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    game.Exit();
        }

        public void Options()
        {
            MouseControlsBtn.Update(new Vector2(screensize_pos.X / 2 - 196, screensize_pos.Y / 2));
            if (player.controls == Player.EControls.Mouse)
                MouseControlsBtn.ButtonEnabled = true;
            else
                MouseControlsBtn.ButtonEnabled = false;

            if (MouseControlsBtn.collision.checkcollision(MouseControlsBtn.GetMousePos, MouseControlsBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    player.controls = Player.EControls.Mouse;

            KeyboardControlsBtn.Update(new Vector2(screensize_pos.X / 2 + 196, screensize_pos.Y / 2));
            if (player.controls == Player.EControls.Keyboard)
                KeyboardControlsBtn.ButtonEnabled = true;
            else
                KeyboardControlsBtn.ButtonEnabled = false;

            if (KeyboardControlsBtn.collision.checkcollision(KeyboardControlsBtn.GetMousePos, KeyboardControlsBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    player.controls = Player.EControls.Keyboard;

            BackBtn.Update(new Vector2(100, 100));
            if (BackBtn.collision.checkcollision(BackBtn.GetMousePos, BackBtn.sprite.sprite_colbox))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    menuMode = EMenuMode.Menu;
        }

        public void Draw(SpriteBatch load_spriteBatch)
        {

            if (gamemode.Mode == GameMode.EGameMode.MENU)
            {
                if (menuMode == EMenuMode.Menu)
                {
                    test.Draw(load_spriteBatch, new Vector2(100,100));

                    mBackground.SpriteDrawContent(load_spriteBatch);
                    Font_Title.Draw_Large_Font(load_spriteBatch, "-1915-", 0.8f, Color.Black);

                    //buttons
                    StartGameBtn.Draw(load_spriteBatch);
                    OptionsBtn.Draw(load_spriteBatch);
                    QuitBtn.Draw(load_spriteBatch);
                }

                if (menuMode == EMenuMode.Options)
                {
                    MouseControlsBtn.Draw(load_spriteBatch);
                    KeyboardControlsBtn.Draw(load_spriteBatch);
                    BackBtn.Draw(load_spriteBatch);

                    Font_Controls.Draw_Large_Font(load_spriteBatch, "Controls", 0.8f, Color.Black);
                }
            }


        }


    }
}
