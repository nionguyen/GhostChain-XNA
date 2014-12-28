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
    class ModeScreen : GameScreen
    {
        #region Fields

        Texture2D backgroundTexture;
        Sprite m_pHowToPlay;
        private GestureSample Gestures;
        private ModeScreenState m_state = ModeScreenState.Select;

        Rectangle EasyButton = new Rectangle(100, 162, 259, 72);
        Rectangle NormalButton = new Rectangle(64, 307, 348, 64);
        Rectangle levelsButton = new Rectangle(78, 438, 320, 128);
        Rectangle howToPlayButton = new Rectangle(59, 595, 270, 39);

        /* chinese
        Rectangle EasyButton = new Rectangle(120, 167, 261, 87);
        Rectangle NormalButton = new Rectangle(114, 292, 264, 93);
        Rectangle levelsButton = new Rectangle(118, 468, 264, 88);
        Rectangle howToPlayButton = new Rectangle(57, 598, 138, 45);
        */
        #endregion

        #region Initialization

        public ModeScreen()
        {
            //SoundManager.StopSongs();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            backgroundTexture = screenContent.Load<Texture2D>("BG_Mode");
            m_pHowToPlay = ResourceManager.GetInstance().GetSprite("BG_How_To_Play");
            m_pHowToPlay.Depth = 0.5f;
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
                MenuScreen menuScreen = new MenuScreen();
                ScreenManager.AddScreen(menuScreen);
                this.ExitScreen();
                return;
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
                if (m_state == ModeScreenState.Select)
                {
                    if (EasyButton.Contains(posTap))
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        MainGameScreen mainGameScreen = new MainGameScreen();
                        mainGameScreen.SetModeGame(GameMode.EasyClassic);
                        ScreenManager.AddScreen(mainGameScreen);
                        this.ExitScreen();
                        return;
                    }
                    if (NormalButton.Contains(posTap))
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        MainGameScreen mainGameScreen = new MainGameScreen();
                        mainGameScreen.SetModeGame(GameMode.NormalClassic);
                        ScreenManager.AddScreen(mainGameScreen);
                        this.ExitScreen();
                        return;
                    }
                    if (levelsButton.Contains(posTap))
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        LevelScreen levelScreen = new LevelScreen();
                        ScreenManager.AddScreen(levelScreen);
                        this.ExitScreen();
                        return;
                    }
                    if (howToPlayButton.Contains(posTap))
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        m_state = ModeScreenState.Tutorial;

                        return;
                    }
                }
                if (m_state == ModeScreenState.Tutorial)
                {
                    SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                    m_pHowToPlay.CurentFrame++;
                    m_pHowToPlay.setFrame(m_pHowToPlay.CurentFrame);
                    if (m_pHowToPlay.CurentFrame > m_pHowToPlay.TotalFrame - 1)
                    {
                        m_pHowToPlay.CurentFrame = 0;
                        m_pHowToPlay.setFrame(m_pHowToPlay.CurentFrame);
                        m_state = ModeScreenState.Select;
                    }

                }
            }
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;


            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, Vector2.Zero,  new Rectangle(0, 0, 480, 800), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.1f);
            if (m_state == ModeScreenState.Tutorial)
            {
                m_pHowToPlay.Draw(spriteBatch,new Vector2(49,134));
            }
            spriteBatch.End();
        }


        #endregion
    }
}
