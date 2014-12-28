#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace WindowsPhoneGame2
{
    class MenuScreen : GameScreen
    {
        #region Fields

        Texture2D backgroundTexture;
        private GestureSample Gestures;

        Rectangle startButton = new Rectangle(100, 435, 237, 93);
        Rectangle aboutButton = new Rectangle(208, 545, 243, 81);
        
        /* chinese
        Rectangle startButton = new Rectangle(83, 449, 312, 90);
        Rectangle aboutButton = new Rectangle(263, 580, 134, 80);
        */
        #endregion

        #region Initialization

        public MenuScreen()
        {
            ResourceManager.GetInstance();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            SoundManager.GetInstance().PlaySong(ESong.Background);
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
            if (Global.Back == true)
            {
                SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                ScreenManager.Game.Exit();
            }
            if (TouchPanel.IsGestureAvailable)
            {
                Gestures = TouchPanel.ReadGesture();
            }
            else
            {
                Gestures = new GestureSample();
            }

            if (Gestures.GestureType == GestureType.Tap)
            {
                Point posTap = new Point((int)Gestures.Position.X, (int)Gestures.Position.Y);
                if (startButton.Contains(posTap))
                {
                    SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                    ModeScreen modeScreen = new ModeScreen();
                    ScreenManager.AddScreen(modeScreen);
                    this.ExitScreen();
                    return;
                }
                if (aboutButton.Contains(posTap))
                {
                    SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                    AboutScreen aboutScreen = new AboutScreen();
                    ScreenManager.AddScreen(aboutScreen);
                    this.ExitScreen();
                    return;
                }
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
