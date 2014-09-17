using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1915
{
    class GameMode
    {
        public enum EGameMode
        {
            MENU,
            LEVELSELECT,
            PLAY
        };

        public EGameMode Mode = EGameMode.MENU;

        public GameMode()
        {
            switch (Mode)
            {
                case EGameMode.MENU: ; break; 
                case EGameMode.PLAY: ; break; 
            }
        }
    }
}
