#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Threading;
#endregion

namespace WindowsPhoneGame2
{
    class MainGameScreen : GameScreen
    {
        #region Fields
        Texture2D backgroundTexture;
        //private GestureSample Gestures;
        #endregion
        Sprite m_blankBG;

        Grid m_grid;

        //fps
        SpriteFont _spr_font;
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;

        public MainGameScreen()
        {
            m_grid = new Grid();
        }
        
        public override void LoadContent()
        {
            base.LoadContent();
            backgroundTexture = screenContent.Load<Texture2D>("BG_Game");
            m_blankBG = ResourceManager.GetInstance().GetSprite("BlankScreen");
            m_blankBG.Depth = 0.9f;
            //Gestures = new GestureSample();
            _spr_font = screenContent.Load<SpriteFont>("SpriteFont1");
            m_grid.LoadContent(screenContent);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Global.Back == true)
            {
                SoundManager.PlaySound(ESound.SelectButton);
                MenuScreen menuScreen = new MenuScreen();
                ScreenManager.AddScreen(menuScreen);
                this.ExitScreen();
                return;
            }
            m_grid.Update(gameTime);

            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
 
            // 1 Second has passed
            if (_elapsed_time >= 1000.0f)
            {
                _fps = _total_frames;
                _total_frames = 0;
                _elapsed_time = 0;
            }

            base.Update(gameTime, otherScreenHasFocus, false);
        }

        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            _total_frames++;
            spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend);

            spriteBatch.Draw(backgroundTexture, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            m_grid.Draw(spriteBatch);
            m_blankBG.Draw(spriteBatch, Vector2.Zero);
            spriteBatch.DrawString(_spr_font, _fps.ToString(), new Vector2(10.0f, 600.0f), Color.White);
            
            spriteBatch.End();
            
        }

    }

}
