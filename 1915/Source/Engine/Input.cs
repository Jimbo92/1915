﻿using System;
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
    class Input
    {
        public KeyboardState PreviousKeyboard;
        public KeyboardState CurrentKeyboard;

        public bool KeyboardPressed(Keys key)
        {
            if (CurrentKeyboard.IsKeyDown(key) && PreviousKeyboard.IsKeyUp(key))
                return true;
            else
                return false;
        }
        public bool KeyboardReleased(Keys key)
        {
            if (CurrentKeyboard.IsKeyUp(key) && PreviousKeyboard.IsKeyDown(key))
                return true;
            else
                return false;
        }
        public bool KeyboardPress(Keys key)
        {
            if (CurrentKeyboard.IsKeyDown(key))
                return true;
            else
                return false;
        }
        public bool KeyboardRelease(Keys key)
        {
            if (CurrentKeyboard.IsKeyUp(key))
                return true;
            else
                return false;
        }

        public MouseState PreviousMouse;
        public MouseState CurrentMouse;

        public enum EClicks
        {
            LEFT,
            MIDDLE,
            RIGHT
        }

        public bool ClickPressed(EClicks click)
        {
            switch (click)
            {
                case EClicks.LEFT: return CurrentMouse.LeftButton == ButtonState.Pressed & PreviousMouse.LeftButton == ButtonState.Released;
                case EClicks.MIDDLE: return CurrentMouse.MiddleButton == ButtonState.Pressed & PreviousMouse.MiddleButton == ButtonState.Released;
                case EClicks.RIGHT: return CurrentMouse.RightButton == ButtonState.Pressed & PreviousMouse.RightButton == ButtonState.Released;
            }
            return false;
        }
        public bool ClickPress(EClicks click)
        {
            switch (click)
            {
                case EClicks.LEFT: return CurrentMouse.LeftButton == ButtonState.Pressed;
                case EClicks.MIDDLE: return CurrentMouse.MiddleButton == ButtonState.Pressed;
                case EClicks.RIGHT: return CurrentMouse.RightButton == ButtonState.Pressed;
            }
            return false;
        }
        public bool ClickReleased(EClicks click)
        {
            switch (click)
            {
                case EClicks.LEFT: return CurrentMouse.LeftButton == ButtonState.Released & PreviousMouse.LeftButton == ButtonState.Pressed;
                case EClicks.MIDDLE: return CurrentMouse.MiddleButton == ButtonState.Released & PreviousMouse.MiddleButton == ButtonState.Pressed;
                case EClicks.RIGHT: return CurrentMouse.RightButton == ButtonState.Released & PreviousMouse.RightButton == ButtonState.Pressed;
            }
            return false;
        }
        public bool ClickRelease(EClicks click)
        {
            switch (click)
            {
                case EClicks.LEFT: return CurrentMouse.LeftButton == ButtonState.Released;
                case EClicks.MIDDLE: return CurrentMouse.MiddleButton == ButtonState.Released;
                case EClicks.RIGHT: return CurrentMouse.RightButton == ButtonState.Released;
            }
            return false;
        }

        public void Begin()
        {
            CurrentKeyboard = Keyboard.GetState();
            CurrentMouse = Mouse.GetState();
        }

        public void End()
        {
            PreviousKeyboard = CurrentKeyboard;
            PreviousMouse = CurrentMouse;
        }
    }
}
