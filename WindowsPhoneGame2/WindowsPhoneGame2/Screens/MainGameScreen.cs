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
using Microsoft.Phone.Tasks;
using System.Windows;
#endregion

namespace WindowsPhoneGame2
{
    class MainGameScreen : GameScreen
    {
        #region Fields
        Sprite m_pBG_Game;
        Sprite m_pBlank_Game;
        Sprite m_pPausePopUp;
        Sprite m_pMusicOff;
        Sprite m_pLevelDone;
        Sprite m_pClassicDone;
        Sprite m_pComplete;
        #endregion

        Grid m_grid;

        SpriteFont m_gameFont;

        LevelManager m_levelManager;
        GameTimeManager m_gameTimeManager;

        public PointManager m_pointManager;
        float m_completeTime = 2.0f;


        Vector2 m_completePos = new Vector2(150, 550);
        Vector2 m_AccleComplete = new Vector2(0, -0.22f);

        Rectangle pauseButton = new Rectangle(426, 0, 60, 64);
        Rectangle resumeButton = new Rectangle(70, 269, 246, 59);
        Rectangle restartButton = new Rectangle(70, 360, 256, 67);
        Rectangle menuButton = new Rectangle(83, 444, 203, 71);
        Rectangle musicButton = new Rectangle(362, 217, 62, 62);

        Rectangle classic_MenuButton = new Rectangle(172, 699, 131, 61);
        Rectangle classic_PlayAgainButton = new Rectangle(99, 586, 287, 61);

        Rectangle level_MenuButton = new Rectangle(47, 701, 129, 62);
        Rectangle level_NextLevelButton = new Rectangle(97, 587, 287, 65);
        Rectangle level_RetryButton = new Rectangle(300, 701, 136, 62);
        /*Chinese
        Vector2 m_completePos = new Vector2(150, 550);
        Vector2 m_AccleComplete = new Vector2(0, -0.22f);

        Rectangle pauseButton = new Rectangle(426, 0, 60, 64);
        Rectangle resumeButton = new Rectangle(86, 267, 196, 63);
        Rectangle restartButton = new Rectangle(95, 359, 192, 65);
        Rectangle menuButton = new Rectangle(88, 444, 195, 72);
        Rectangle musicButton = new Rectangle(362, 217, 62, 62);

        Rectangle classic_MenuButton = new Rectangle(140, 680, 161, 57);
        Rectangle classic_PlayAgainButton = new Rectangle(140, 571, 237, 57);

        Rectangle level_MenuButton = new Rectangle(72, 672, 95, 82);
        Rectangle level_NextLevelButton = new Rectangle(154, 567, 182, 67);
        Rectangle level_RetryButton = new Rectangle(312, 672, 91, 73);
        */
        public MainGameScreen()
        {
            m_grid = new Grid();
            m_levelManager = new LevelManager();
            m_gameTimeManager = new GameTimeManager();
            m_pointManager = new PointManager();
        }

        public void SetModeGame(GameMode _mode)
        {
            Global.GAMEMODE = _mode;
            Global.GAMESTATE = GameState.Running;
            m_gameTimeManager.SetTime();
        }
        public void SetLevel(int _level)
        {
            if (_level > m_levelManager.MAXLEVEL)
            {
                LevelScreen levelScreen = new LevelScreen();
                DataManager.GetInstance().CurrentLevel = Global.MAXLEVEL + 1;
                levelScreen.SetToComingSoon();
                ScreenManager.AddScreen(levelScreen);
                this.ExitScreen();
                return;
            }
            m_levelManager.SetLevel(_level);
            m_gameTimeManager.setLimitTime(_level);
        }
        public void Reset()
        {
            m_completeTime = 2.0f;
            m_completePos = new Vector2(100, 550);
            m_grid.Reset(screenContent);
            m_levelManager.Reset();
            m_gameTimeManager.Reset();
            m_pointManager.Reset();
        }
        public void SetPoint(Cell_Type _cellType, int _countCell, Vector2 _posPoint)
        {
            m_pointManager.SetPoint(_cellType, _countCell, _posPoint);
        }
        public void SetTime(Cell_Type _cellType, int _countCell)
        {
            m_gameTimeManager.SetTime(_cellType, _countCell);
        }

        public void SetGoal(Cell_Type _type, int _countCell)
        {
            m_levelManager.SetGoal(_type, _countCell);
        }
        public override void LoadContent()
        {
            base.LoadContent();

            m_pBG_Game = ResourceManager.GetInstance().GetSprite("BG_Game");
            m_pBG_Game.Depth = 0.5f;
            m_pBlank_Game = ResourceManager.GetInstance().GetSprite("Blank_Game");
            m_pBlank_Game.Depth = 0.01f;
            m_pPausePopUp = ResourceManager.GetInstance().GetSprite("PausePopup");
            m_pPausePopUp.Depth = 0.9f;
            m_pMusicOff = ResourceManager.GetInstance().GetSprite("Music_Off");
            m_pMusicOff.Depth = 0.91f;

            m_pLevelDone = ResourceManager.GetInstance().GetSprite("BG_Level_Mode_Done");
            m_pLevelDone.Depth = 0.6f;
            m_pClassicDone = ResourceManager.GetInstance().GetSprite("BG_Classic_Mode_Done");
            m_pClassicDone.Depth = 0.6f;
            m_pComplete = ResourceManager.GetInstance().GetSprite("Complete");
            m_pComplete.Depth = 0.7f;

            m_gameFont = screenContent.Load<SpriteFont>("Font/GameFont");
            m_grid.LoadContent(screenContent, this);

        }

        public override void UnloadContent()
        {
            DataManager.GetInstance().SaveData();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
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
                Microsoft.Xna.Framework.Point posTap = GesturesInput.GetInstance().GetGesturePoint();


                if (pauseButton.Contains(posTap))
                {
                    if (Global.GAMESTATE == GameState.Running)
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        Global.GAMESTATE = GameState.PauseGame;
                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }

                if (resumeButton.Contains(posTap))
                {
                    if (Global.GAMESTATE == GameState.PauseGame)
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        Global.GAMESTATE = GameState.Running;
                        m_grid.DragCompleteHandle();
                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }

                if (restartButton.Contains(posTap))
                {
                    if (Global.GAMESTATE == GameState.PauseGame)
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        Reset();
                        Global.GAMESTATE = GameState.Running;
                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }

                if (menuButton.Contains(posTap))
                {
                    if (Global.GAMESTATE == GameState.PauseGame)
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        MenuScreen menuScreen = new MenuScreen();
                        ScreenManager.AddScreen(menuScreen);
                        this.ExitScreen();
                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }
                if (musicButton.Contains(posTap))
                {
                    if (Global.GAMESTATE == GameState.PauseGame)
                    {
                        SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                        if (Global.SOUND == true)
                        {
                            SoundManager.GetInstance().StopSongs();
                            //SoundManager.GetInstance().PauseSong();
                            Global.SOUND = false;
                        }
                        else
                        {
                            Global.SOUND = true;
                            SoundManager.GetInstance().PlaySong(ESong.Background);
                            //SoundManager.GetInstance().ResumeSong();
                        }


                        GesturesInput.GetInstance().Reset();
                        return;
                    }
                }

                if (Global.GAMESTATE == GameState.EndGame)
                {

                    if (Global.GAMEMODE == GameMode.EasyClassic || Global.GAMEMODE == GameMode.NormalClassic)
                    {
                        if (classic_MenuButton.Contains(posTap))
                        {
                            SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                            MenuScreen menuScreen = new MenuScreen();
                            ScreenManager.AddScreen(menuScreen);
                            this.ExitScreen();
                            GesturesInput.GetInstance().Reset();
                            return;
                        }
                        if (classic_PlayAgainButton.Contains(posTap))
                        {
                            SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                            Reset();
                            Global.GAMESTATE = GameState.Running;
                            GesturesInput.GetInstance().Reset();
                            return;
                        }
                    }
                    if (Global.GAMEMODE == GameMode.Level)
                    {
                        if (level_MenuButton.Contains(posTap))
                        {

                            SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                            MenuScreen menuScreen = new MenuScreen();
                            ScreenManager.AddScreen(menuScreen);
                            this.ExitScreen();
                            GesturesInput.GetInstance().Reset();
                            return;
                        }
                        if (level_NextLevelButton.Contains(posTap))
                        {
                            SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                            int newLevel = m_levelManager.m_currentLevel + 1;
                            m_levelManager.NewLevel();
                            SetLevel(newLevel);
                            Reset();
                            Global.GAMESTATE = GameState.Running;
                            GesturesInput.GetInstance().Reset();
                            return;
                        }
                        if (level_RetryButton.Contains(posTap))
                        {
                            SoundManager.GetInstance().PlaySound(ESound.SelectButton);
                            Reset();
                            Global.GAMESTATE = GameState.Running;
                            GesturesInput.GetInstance().Reset();
                            return;
                        }

                    }
                }


            }
            m_grid.Update(gameTime);
            m_gameTimeManager.Update(gameTime);
            m_pointManager.Update(gameTime);
            if (Global.GAMEMODE == GameMode.Level)
            {
                if (Global.GAMESTATE == GameState.Running && m_levelManager.isWin())
                {
                    SoundManager.GetInstance().PlaySound(ESound.LevelComplete);
                    Global.GAMESTATE = GameState.BeforeEndGame;
                    return;
                }
            }

            if (Global.GAMEMODE == GameMode.EasyClassic || Global.GAMEMODE == GameMode.NormalClassic)
            {
                if (Global.GAMESTATE == GameState.Running && m_gameTimeManager.IsLose())
                {
                    Global.GAMESTATE = GameState.BeforeEndGame;
                    SoundManager.GetInstance().PlaySound(ESound.ClassicComplete);
                    return;
                }
            }

            if (Global.GAMESTATE == GameState.BeforeEndGame)
            {
                m_completeTime -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                m_completePos += m_AccleComplete * (float)gameTime.ElapsedGameTime.Milliseconds;
                if (m_completeTime <= 0)
                {
                    Global.GAMESTATE = GameState.EndGame;
                    DataManager.GetInstance().LevelCount++;
                    if ((DataManager.GetInstance().LevelCount % 5) == 0)
                    {
                        if (MessageBox.Show("YOUR SMILE IS OUR GOAL. okay?", "Do you enjoy this game? Then rate 5 star to favor us", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                            marketplaceReviewTask.Show();
                        }
                    }
                    m_completeTime = 0;
                    if (Global.GAMEMODE == GameMode.Level)
                    {
                        int newLevel = m_levelManager.m_currentLevel + 1;
                        if (newLevel > DataManager.GetInstance().CurrentLevel)
                            DataManager.GetInstance().CurrentLevel = newLevel;
                    }
                }
            }
            GesturesInput.GetInstance().Reset();
            base.Update(gameTime, otherScreenHasFocus, false);
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null);
            m_pBG_Game.Draw(spriteBatch);
            m_pBlank_Game.Draw(spriteBatch);

            m_grid.Draw(spriteBatch);
            m_pointManager.Draw(spriteBatch, m_gameFont);
            m_gameTimeManager.Draw(spriteBatch, m_gameFont);
            if (Global.GAMESTATE == GameState.PauseGame)
                m_pPausePopUp.Draw(spriteBatch);
            if (Global.GAMESTATE == GameState.EndGame)
            {
                if (Global.GAMEMODE == GameMode.EasyClassic)
                {
                    m_pClassicDone.Draw(spriteBatch);
                    //spriteBatch.DrawString(m_gameFont, "Complete Easy Mode", new Vector2(30, 156), Color.White, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, m_pointManager.m_totalPoint.ToString(), new Vector2(120, 240), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, DataManager.GetInstance().EasyHighScore.ToString(), new Vector2(150, 327), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                }
                if (Global.GAMEMODE == GameMode.NormalClassic)
                {
                    m_pClassicDone.Draw(spriteBatch);
                    //spriteBatch.DrawString(m_gameFont, "Complete Normal Mode", new Vector2(30, 156), Color.White, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, m_pointManager.m_totalPoint.ToString(), new Vector2(120, 240), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, DataManager.GetInstance().NormalHighScore.ToString(), new Vector2(150, 327), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                }
                if (Global.GAMEMODE == GameMode.Level)
                {
                    m_pLevelDone.Draw(spriteBatch);
                    spriteBatch.DrawString(m_gameFont, m_levelManager.m_currentLevel.ToString(), new Vector2(153, 153), Color.White, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, m_pointManager.m_totalPoint.ToString(), new Vector2(122, 240), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                    int bonus = 100 * m_gameTimeManager.GetTimeBonus();
                    int total = bonus + m_pointManager.m_totalPoint;
                    spriteBatch.DrawString(m_gameFont, bonus.ToString(), new Vector2(122, 327), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                    spriteBatch.DrawString(m_gameFont, total.ToString(), new Vector2(122, 414), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.9f);
                }
            }
            if (Global.GAMESTATE == GameState.BeforeEndGame)
            {
                m_pComplete.Draw(spriteBatch, m_completePos);
                //spriteBatch.DrawString(m_gameFont, "Complete", m_completePos, Color.Yellow, 0.0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0.7f);
            }
            if (Global.GAMEMODE == GameMode.Level)
            {

                //LevelManager.GetInstance().Draw(spriteBatch, m_goalFont);
                m_levelManager.Draw(spriteBatch, m_gameFont);
            }



            if (!Global.SOUND)
            {
                if (Global.GAMESTATE == GameState.PauseGame)
                    m_pMusicOff.Draw(spriteBatch, new Vector2(362, 217));
            }

            spriteBatch.End();

        }

    }

}