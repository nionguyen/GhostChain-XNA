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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace WindowsPhoneGame2
{
    class MenuScreen : GameScreen
    {
        #region Fields

        Texture2D backgroundTexture;
        private GestureSample Gestures;
        #endregion

        #region Initialization

        public MenuScreen()
        {
            SoundManager.StopSongs();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundTexture = screenContent.Load<Texture2D>("BG_Menu");
            Gestures = new GestureSample();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }


        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            if (TouchPanel.IsGestureAvailable)
            {
                Gestures = TouchPanel.ReadGesture();
            }
            else
            {
                Gestures = new GestureSample();
            }

            if (Gestures.GestureType == GestureType.Tap || (Global.Back == true))
            {
                SoundManager.PlaySound(ESound.SelectButton);
                MainGameScreen mainGameScreen = new MainGameScreen();
                ScreenManager.AddScreen(mainGameScreen);
                this.ExitScreen();
                return;
            }
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, fullscreen,
                             new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            spriteBatch.End();
        }


        #endregion
    }
}
