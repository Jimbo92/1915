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
    class Level_Select
    {
        //Button Varibles
        //Back Button
        Button BackBtn = new Button();
        //Level Select Buttons
        Button Lvl1Btn = new Button();

        Button UpBtn = new Button();
        Button WepUpBtn = new Button();

        //Upgrade Costs
        int WeaponUpCost = 1000;

        //Misc Varibles
        Vector2 ScreenSize;
        GameMode gameMode;
        Game1 game1;
        Player player;
        Input input;

        //Font
        Fonts Font_Score;
        Fonts Font_WepUpgrade;
        Fonts Font_WepUpgradeLvl;

        Fonts Font_CanUpgrade;
        string WepUpgrade;

        //Menu Modes
        enum Emodes
        {
            LvlSelect,
            Upgrade
        };
        Emodes Mode = Emodes.LvlSelect;

        public void LoadContent(ContentManager load_content, Vector2 load_screensize, GameMode load_gamemode, Player load_player)
        {
            //Misc
            ScreenSize = load_screensize;
            gameMode = load_gamemode;
            player = load_player;

            //Fonts
            Font_Score = new Fonts(load_content, ScreenSize.X / 2, 100);
            Font_WepUpgrade = new Fonts(load_content, ScreenSize.X / 2, ScreenSize.Y / 2 - 100);
            Font_WepUpgradeLvl = new Fonts(load_content, ScreenSize.X / 2, ScreenSize.Y / 2 - 50);

            Font_CanUpgrade = new Fonts(load_content, ScreenSize.X / 2 - 165, ScreenSize.Y - 100);

            //Buttons
            //Back Button
            BackBtn.LoadContentOverlay(load_content, "graphics/menu/menubutton", "graphics/menu/underline", 128, 64);
            BackBtn.FontSize = 1f;
            BackBtn.FontColour = Color.Black;
            BackBtn.ButtonName = "Back";
            //Level 1 Button
            Lvl1Btn.LoadContentOverlay(load_content, "graphics/menu/menubutton", "graphics/menu/underline", 296, 64);
            Lvl1Btn.FontSize = 1f;
            Lvl1Btn.FontColour = Color.Black;
            Lvl1Btn.ButtonName = "1: First Strike";

            //Upgrades Button
            UpBtn.LoadContentOverlay(load_content, "graphics/menu/menubutton", "graphics/menu/underline", 296, 64);
            UpBtn.FontSize = 1f;
            UpBtn.FontColour = Color.Black;
            UpBtn.ButtonName = "Buy Upgrades";
            //Weapon Upgrade Button
            WepUpBtn.LoadContentOverlay(load_content, "graphics/menu/menubutton", "graphics/menu/underline", 256, 64);
            WepUpBtn.FontSize = 1f;
            WepUpBtn.FontColour = Color.Black;
            WepUpBtn.ButtonName = "Level Up";

        }

        public void Update(Game1 load_game, Input load_input)
        {
            //Misc
            game1 = load_game;
            input = load_input;

            //Universal Buttons
            //Back Button
            BackBtn.Update(new Vector2(100, 100));
            if (BackBtn.collision.checkcollision(BackBtn.sprite.sprite_colbox, BackBtn.GetMousePos))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    if (Mode == Emodes.LvlSelect)
                        gameMode.Mode = GameMode.EGameMode.MENU;
                    else
                        Mode = Emodes.LvlSelect;

            //Upgrade Button Enabled
            //Weapon Upgrade
            if (game1.UniScore < WeaponUpCost || player.weapons == Player.EWeapons.Ultimate)
                WepUpBtn.ButtonDisabled = true;
            else
                WepUpBtn.ButtonDisabled = false;


            //Level Select Modes
            switch (Mode)
            {
                case Emodes.LvlSelect: LevelSelect(); break;
                case Emodes.Upgrade: Upgrades(); break;
            }
        }

        public void LevelSelect()
        {
            //Buttons
            //Upgrades Button
            UpBtn.Update(new Vector2(ScreenSize.X / 2, ScreenSize.Y - 100));
            if (UpBtn.collision.checkcollision(UpBtn.sprite.sprite_colbox, UpBtn.GetMousePos))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    Mode = Emodes.Upgrade;
            //Level 1 Button
            Lvl1Btn.Update(new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2 - 64));
            if (Lvl1Btn.collision.checkcollision(Lvl1Btn.sprite.sprite_colbox, Lvl1Btn.GetMousePos))
                if (input.ClickReleased(Input.EClicks.LEFT))
                    gameMode.Mode = GameMode.EGameMode.PLAY;

        }

        public void Upgrades()
        {
            //Buttons
            //Weapon Upgrade Button
            WepUpBtn.Update(new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2));

            if (WepUpBtn.collision.checkcollision(WepUpBtn.sprite.sprite_colbox, WepUpBtn.GetMousePos) && WepUpBtn.ButtonDisabled != true)
            {
                if (input.ClickReleased(Input.EClicks.LEFT))
                {
                    if (game1.UniScore >= WeaponUpCost && player.weapons == Player.EWeapons.Basic)
                    {

                        game1.UniScore -= WeaponUpCost;
                        player.weapons = Player.EWeapons.Advanced;
                        WeaponUpCost = 2000;
                    }
                    else if (game1.UniScore >= WeaponUpCost && player.weapons == Player.EWeapons.Advanced)
                    {

                        game1.UniScore -= WeaponUpCost;
                        player.weapons = Player.EWeapons.Extreme;
                        WeaponUpCost = 3000;
                    }
                    else if (game1.UniScore >= WeaponUpCost && player.weapons == Player.EWeapons.Extreme)
                    {

                        game1.UniScore -= WeaponUpCost;
                        player.weapons = Player.EWeapons.Ultimate;
                        WeaponUpCost = 0;

                    }
                }
            }

        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            //Universal Buttons
            BackBtn.Draw(load_spriteBatch);

            if (Mode == Emodes.LvlSelect)
            {
                //Buttons
                Lvl1Btn.Draw(load_spriteBatch);
                UpBtn.Draw(load_spriteBatch);

                //Fonts
                if(WepUpBtn.ButtonDisabled != true)
                Font_CanUpgrade.Draw_Medium_Font(load_spriteBatch, "!", 2f, Color.Gold);
            }
            else
            {
                //Fonts
                Font_Score.Draw_Medium_Font(load_spriteBatch, "Points: " + game1.UniScore.ToString(), 1f, Color.Black);
                
                Font_WepUpgrade.Draw_Medium_Font(load_spriteBatch, "Weapon Upgrade", 1f, Color.Black);

                switch (player.weapons)
                {
                    case Player.EWeapons.Basic:     WepUpgrade = "Level 1"; break;
                    case Player.EWeapons.Advanced:  WepUpgrade = "Level 2"; break;
                    case Player.EWeapons.Extreme:   WepUpgrade = "Level 3"; break;
                    case Player.EWeapons.Ultimate:  WepUpgrade = "Max"; break;
                }

                Font_WepUpgradeLvl.Draw_Small_Font(load_spriteBatch, WepUpgrade, 1.5f, Color.Black);

                //Buttons
                WepUpBtn.Draw(load_spriteBatch);
            
            }
        }
    }
}
