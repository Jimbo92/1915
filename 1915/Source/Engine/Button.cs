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
    class Button
    {
        enum EButtonType
        {
            Basic,
            Overlay
        };

        public Vector2 position;
        public Sprite sprite = new Sprite();
        public Sprite overlay = new Sprite();

        private Fonts font;
        public string ButtonName;
        public float FontSize;
        public Color FontColour;

        public CollisionCheck collision = new CollisionCheck();
        public Rectangle GetMousePos;

        public bool ButtonEnabled = false;
        public bool ButtonDisabled = false;

        EButtonType BtnType = EButtonType.Basic;

        public void LoadContentBasic(ContentManager load_Content, string load_Texture, int Width, int Height)
        {
            sprite.SpriteLoadContent(load_Texture, load_Content, position.X, position.Y, Width, Height);

            font = new Fonts(load_Content, position.X, position.Y);

            BtnType = EButtonType.Basic;
        }

        public void LoadContentOverlay(ContentManager load_Content, string load_Texture, string load_Overlay, int Width, int Height)
        {
            sprite.SpriteLoadContent(load_Texture, load_Content, position.X, position.Y, Width, Height);

            overlay.SpriteLoadContent(load_Overlay, load_Content, position.X, position.Y, Width, Height);

            font = new Fonts(load_Content, position.X, position.Y);

            BtnType = EButtonType.Overlay;
        }

        public void Update(Vector2 load_position)
        {
            position = load_position;

            sprite.sprite_pos = position;
            overlay.sprite_pos = position;
            font.font_pos = position;

            sprite.SpriteSetColbox2();

            GetMousePos.X = Mouse.GetState().X;
            GetMousePos.Y = Mouse.GetState().Y;

            switch (BtnType)
            {
                case EButtonType.Basic: BasicButton(); break;
                case EButtonType.Overlay: OverlayButton(); break;
            }

        }

        private void BasicButton()
        {
            if (collision.checkcollision(GetMousePos, sprite.sprite_colbox) && !ButtonDisabled)
            {
                sprite.sprite_pos.X = position.X - 2;
                sprite.sprite_pos.Y = position.Y;
                font.font_pos.X = sprite.sprite_pos.X;
                font.font_pos.Y = sprite.sprite_pos.Y;
            }
            else
            {
                sprite.sprite_pos.X = position.X;
                sprite.sprite_pos.Y = position.Y;
                font.font_pos.X = sprite.sprite_pos.X;
                font.font_pos.Y = sprite.sprite_pos.Y;
            }
        }

        private void OverlayButton()
        {
            overlay.SpriteSetColbox2();
        }

        public void Draw(SpriteBatch load_spriteBatch)
        {
            sprite.SpriteDrawContent(load_spriteBatch);

            if (!ButtonDisabled)
            {
                font.Draw_Medium_Font(load_spriteBatch, ButtonName, FontSize, FontColour);

                if (collision.checkcollision(GetMousePos, sprite.sprite_colbox) || ButtonEnabled)
                {
                    overlay.SpriteDrawContent(load_spriteBatch);
                }
            }
            else
                font.Draw_Medium_Font(load_spriteBatch, ButtonName, FontSize, Color.Gray);
        }
    }
}
