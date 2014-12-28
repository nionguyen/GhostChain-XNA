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
using System.Collections.Generic;
#endregion

namespace WindowsPhoneGame2
{
    class LevelScreen : GameScreen
    {
        #region Fields

        Rectangle backToMenuButton = new Rectangle(72, 735, 332, 54);
        /* chinese
        Rectangle backToMenuButton = new Rectangle(11, 733, 256, 55);
        */
        Sprite m_pBG;
        Sprite m_pLevelSelect;
        Sprite m_pCurrentLevelGrid;
        Sprite m_pLockLevel;
        Rectangle _srcRect;

        int VIEWPORT_WIDTH = 480;
        int MAXPOS_X = 960;

        float VELOC_X = 2.0f;
        float m_MovingX = 0.0f;
        float m_VelocX = 0.0f;
        LevelScreenState m_state = LevelScreenState.Wait;
        Vector2 m_currentLevel = Vector2.Zero;
        int m_level = DataManager.GetInstance().CurrentLevel;
        #endregion
        #region Initialization
        List<Rectangle> m_buttonLevelList = new List<Rectangle>();
        public LevelScreen()
        {
            LoadRect();
            _srcRect = new Rectangle(0, 0, 380, 500);
            if (m_level > Global.MAXLEVEL)
            {
                m_level = Global.MAXLEVEL;
            }
            UpdateCurrentLevelGrid();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            m_pBG = ResourceManager.GetInstance().GetSprite("BG_Level");
            m_pBG.Depth = 0.1f;

            m_pLevelSelect = ResourceManager.GetInstance().GetSprite("BG_Level_Select");
            m_pLevelSelect.Depth = 0.2f;
            m_pCurrentLevelGrid = ResourceManager.GetInstance().GetSprite("Current_Level_Grid");
            m_pCurrentLevelGrid.Depth = 0.2f;
            m_pLockLevel = ResourceManager.GetInstance().GetSprite("Level_Locked");
            m_pLockLevel.Depth = 0.3f;
            //Gestures = new GestureSample();
            
            
        }
        public void SetToComingSoon()
        {
            MovingScreen(MAXPOS_X);
        }
        public void LoadRect()
        {
            Rectangle Level01_Button = new Rectangle(55, 151, 130, 139);
            Rectangle Level02_Button = new Rectangle(175, 151, 130, 139);
            Rectangle Level03_Button = new Rectangle(295, 151, 130, 139);

            Rectangle Level04_Button = new Rectangle(55, 267, 130, 139);
            Rectangle Level05_Button = new Rectangle(175, 267, 130, 139);
            Rectangle Level06_Button = new Rectangle(295, 267, 130, 139);

            Rectangle Level07_Button = new Rectangle(55, 383, 130, 139);
            Rectangle Level08_Button = new Rectangle(175, 383, 130, 139);
            Rectangle Level09_Button = new Rectangle(295, 383, 130, 139);

            Rectangle Level10_Button = new Rectangle(55, 499, 130, 139);
            Rectangle Level11_Button = new Rectangle(175, 499, 130, 139);
            Rectangle Level12_Button = new Rectangle(295, 499, 130, 139);

            //page 2: 13-->24

            Rectangle Level13_Button = new Rectangle(535, 151, 130, 139);
            Rectangle Level14_Button = new Rectangle(655, 151, 130, 139);
            Rectangle Level15_Button = new Rectangle(775, 151, 130, 139);

            Rectangle Level16_Button = new Rectangle(535, 267, 130, 139);
            Rectangle Level17_Button = new Rectangle(655, 267, 130, 139);
            Rectangle Level18_Button = new Rectangle(775, 267, 130, 139);

            Rectangle Level19_Button = new Rectangle(535, 383, 130, 139);
            Rectangle Level20_Button = new Rectangle(655, 383, 130, 139);
            Rectangle Level21_Button = new Rectangle(775, 383, 130, 139);

            Rectangle Level22_Button = new Rectangle(535, 499, 130, 139);
            Rectangle Level23_Button = new Rectangle(655, 499, 130, 139);
            Rectangle Level24_Button = new Rectangle(775, 499, 130, 139);

            m_buttonLevelList.Add(Level01_Button);
            m_buttonLevelList.Add(Level02_Button);
            m_buttonLevelList.Add(Level03_Button);
            m_buttonLevelList.Add(Level04_Button);
            m_buttonLevelList.Add(Level05_Button);
            m_buttonLevelList.Add(Level06_Button);
            m_buttonLevelList.Add(Level07_Button);
            m_buttonLevelList.Add(Level08_Button);
            m_buttonLevelList.Add(Level09_Button);
            m_buttonLevelList.Add(Level10_Button);
            m_buttonLevelList.Add(Level11_Button);
            m_buttonLevelList.Add(Level12_Button);
            m_buttonLevelList.Add(Level13_Button);
            m_buttonLevelList.Add(Level14_Button);
            m_buttonLevelList.Add(Level15_Button);
            m_buttonLevelList.Add(Level16_Button);
            m_buttonLevelList.Add(Level17_Button);
            m_buttonLevelList.Add(Level18_Button);
            m_buttonLevelList.Add(Level19_Button);
            m_buttonLevelList.Add(Level20_Button);
            m_buttonLevelList.Add(Level21_Button);
            m_buttonLevelList.Add(Level22_Button);
            m_buttonLevelList.Add(Level23_Button);
            m_buttonLevelList.Add(Level24_Button);

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
                ModeScreen modeScreen = new ModeScreen();
                ScreenManager.AddScreen(modeScreen);
                this.ExitScreen();
                return;
            }

            GesturesInput.GetInstance().Update(gameTime);
            if (GesturesInput.GetInstance().GetGestureType() == GestureType.Tap)
            {
                Point posTap = GesturesInput.GetInstance().GetGesturePoint();

                if (backToMenuButton.Contains(posTap))
                {
                    SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                    MenuScreen menuScreen = new MenuScreen();
                    ScreenManager.AddScreen(menuScreen);
                    this.ExitScreen();
                    GesturesInput.GetInstance().Reset();
                    return;
                }

                for (int i = 0; i < m_level; i++)
                {
                    if (m_buttonLevelList[i].Contains(posTap))
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        MainGameScreen mainGameScreen = new MainGameScreen();
                        mainGameScreen.SetModeGame(GameMode.Level);
                        int level = i + 1;
                        mainGameScreen.SetLevel(level);
                        ScreenManager.AddScreen(mainGameScreen);
                        GesturesInput.GetInstance().Reset();
                        this.ExitScreen();
                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }
            }
            if (GesturesInput.GetInstance().GetDirDrag() == DirDrag.Left_Right)
            {
                if (m_state != LevelScreenState.Moving)
                {
                    m_state = LevelScreenState.Moving;
                    m_VelocX = -VELOC_X;
                    m_MovingX = 0;
                    GesturesInput.GetInstance().Reset();
                }
            }

            if (GesturesInput.GetInstance().GetDirDrag() == DirDrag.Right_Left)
            {
                if (m_state != LevelScreenState.Moving)
                {
                    m_state = LevelScreenState.Moving;
                    m_VelocX = VELOC_X;
                    m_MovingX = 0;
                    GesturesInput.GetInstance().Reset();
                }
            }

            if (m_state == LevelScreenState.Moving)
            {
                MovingScreen(-(int)m_MovingX);
                m_MovingX += m_VelocX * (float)gameTime.ElapsedGameTime.Milliseconds;

                if (m_VelocX > 0)
                {
                    if (m_MovingX >= VIEWPORT_WIDTH)
                    {
                        m_state = LevelScreenState.Wait;
                        MovingScreen(VIEWPORT_WIDTH);
                        m_MovingX = 0;
                        m_VelocX = 0;

                    }
                    else MovingScreen((int)m_MovingX);
                }
                if (m_VelocX < 0)
                {
                    if (m_MovingX <= -VIEWPORT_WIDTH)
                    {
                        m_state = LevelScreenState.Wait;
                        MovingScreen(-VIEWPORT_WIDTH);
                        m_MovingX = 0;
                        m_VelocX = 0;
                        
                    }
                    else MovingScreen((int)m_MovingX);
                }
                if (_srcRect.X < 0)
                {
                    int BonusDeltaX = _srcRect.X - 0;
                    MovingScreen(-BonusDeltaX);
                }
                if (_srcRect.X > MAXPOS_X)
                {
                    int BonusDeltaX = _srcRect.X - MAXPOS_X;
                    MovingScreen(-BonusDeltaX);
                }
            }
            UpdateCurrentLevelGrid();
            base.Update(gameTime, otherScreenHasFocus, false);
        }
        public void UpdateCurrentLevelGrid()
        {
            if (_srcRect.X < 480)
                m_currentLevel = new Vector2(166, 651);
            if (_srcRect.X >= 480 && _srcRect.X < 960)
                m_currentLevel = new Vector2(216, 651);
            if (_srcRect.X >= 960 && _srcRect.X < 1440)
                m_currentLevel = new Vector2(266, 651);
        }
        public void MovingScreen(int delta)
        {
            _srcRect.X += delta;

            for (int i = 0; i < m_buttonLevelList.Count; i++)
            {
                Rectangle a = m_buttonLevelList[i];
                a.X -= delta;
                m_buttonLevelList[i] = a;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            m_pBG.Draw(spriteBatch);
            m_pLevelSelect.Draw(spriteBatch, _srcRect, new Rectangle(49, 144, 380, 500));
            m_pCurrentLevelGrid.Draw(spriteBatch, m_currentLevel);
            for (int i = m_level; i < m_buttonLevelList.Count; i++)
            {
                Rectangle a = m_buttonLevelList[i];
                m_pLockLevel.Draw(spriteBatch,new Rectangle(0,0,130,139), a);
            }
            spriteBatch.End();
        }




        #endregion
    }
}
