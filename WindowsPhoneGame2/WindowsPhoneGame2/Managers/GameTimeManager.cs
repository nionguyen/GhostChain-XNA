using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame2
{
    public class GameTimeManager
    {
        Vector2 m_timePos = new Vector2(354, 66);
        float m_gameTime;

        float m_limitTime = 0;
        bool isTime4 = false;
        bool isTime3 = false;
        bool isTime2 = false;
        bool isTime1 = false;
        bool isTime0 = false;
        float bonusTime = 0.0f;
        bool isBonusTime = false;
        float CountTime = 0.0f;
        float BONUSTIME = 0.5f;
        Vector2 m_bonusTimePos = new Vector2(270, 85);
        public GameTimeManager()
        {
        }

        public void setLimitTime(int _level)
        {
            switch (_level)
            {
                case 1:
                    m_limitTime = 60;
                    break;
                case 2:
                    m_limitTime = 80;
                    break;
                case 3:
                    m_limitTime = 90;
                    break;
                case 4:
                    m_limitTime = 120;
                    break;
                case 5:
                    m_limitTime = 150;
                    break;
                case 6:
                    m_limitTime = 180;
                    break;
                case 7:
                    m_limitTime = 210;
                    break;
                case 8:
                    m_limitTime = 240;
                    break;
                case 9:
                    m_limitTime = 270;
                    break;
                case 10:
                    m_limitTime = 300;
                    break;
                case 11:
                    m_limitTime = 360;
                    break;
                case 12:
                    m_limitTime = 390;
                    break;
                case 13:
                    m_limitTime = 420;
                    break;
                case 14:
                    m_limitTime = 450;
                    break;
                case 15:
                    m_limitTime = 480;
                    break;
                case 16:
                    m_limitTime = 510;
                    break;
                case 17:
                    m_limitTime = 540;
                    break;
                case 18:
                    m_limitTime = 570;
                    break;
                case 19:
                    m_limitTime = 600;
                    break;
                case 20:
                    m_limitTime = 630;
                    break;
                case 21:
                    m_limitTime = 660;
                    break;
                case 22:
                    m_limitTime = 690;
                    break;
                case 23:
                    m_limitTime = 710;
                    break;
                case 24:
                    m_limitTime = 720;
                    break;
                default:
                    m_limitTime = 720;
                    break;
            }
        }


        public void SetTime(Cell_Type _cellType, int _countCell)
        {
            bonusTime = 0.0f;
            if (Global.GAMEMODE != GameMode.Level)
            {
                switch (_cellType)
                {
                    case Cell_Type.Type06:
                        bonusTime = 1.0f * _countCell;
                        isBonusTime = true;
                        CountTime = BONUSTIME;
                        break;
                    case Cell_Type.Type07:
                        bonusTime = 2.0f * _countCell;
                        isBonusTime = true;
                        CountTime = BONUSTIME;
                        break;
                    case Cell_Type.Type08:
                        bonusTime = 4.0f * _countCell;
                        isBonusTime = true;
                        CountTime = BONUSTIME;
                        break;
                }
                m_gameTime += bonusTime;
            }
        }

        public int GetTimeBonus()
        {
            int tempTime = (int)(m_limitTime - m_gameTime);
            if(tempTime <= 0)
                tempTime = 0;
            return tempTime;
        }
        public void Reset()
        {
            SetTime();
        }
        public void SetTime()
        {
            isTime0 = false;
            isTime1 = false;
            isTime2 = false;
            isTime3 = false;
            isTime4 = false;

            if (Global.GAMEMODE == GameMode.Level)
            {
                m_gameTime = 0;
            }
            if (Global.GAMEMODE == GameMode.EasyClassic)
            {
                m_gameTime = 120;
            }
            if (Global.GAMEMODE == GameMode.NormalClassic)
            {
                m_gameTime = 120;
            }
        }

        public bool IsLose()
        {
            if (m_gameTime <= 0)
                return true;
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (Global.GAMEMODE == GameMode.Level)
            {
                if (Global.GAMESTATE == GameState.Running)
                    m_gameTime += (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            if (Global.GAMEMODE == GameMode.EasyClassic || Global.GAMEMODE == GameMode.NormalClassic)
            {
                if (Global.GAMESTATE == GameState.Running)
                {
                    if (isBonusTime)
                    {
                        CountTime -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                        m_bonusTimePos += new Vector2(0, -0.01f * gameTime.ElapsedGameTime.Milliseconds);
                        if (CountTime <= 0.0f)
                        {
                            CountTime = 0.0f;
                            isBonusTime = false;
                        }
                    }
                    m_gameTime -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                    int tempTime = (int)m_gameTime;
                    if (tempTime == 4 && !isTime4)
                    {
                        isTime4 = true;
                        SoundManager.GetInstance().PlaySound(ESound.TimeUp);
                    }
                    if (tempTime == 3 && !isTime3)
                    {
                        isTime3 = true;
                        SoundManager.GetInstance().PlaySound(ESound.TimeUp);
                    }
                    if (tempTime == 2 && !isTime2)
                    {
                        isTime2 = true;
                        SoundManager.GetInstance().PlaySound(ESound.TimeUp);
                    }
                    if (tempTime == 1 && !isTime1)
                    {
                        isTime1 = true;
                        SoundManager.GetInstance().PlaySound(ESound.TimeUp);
                    }
                    if (tempTime == 0 && !isTime0)
                    {
                        isTime0 = true;
                        SoundManager.GetInstance().PlaySound(ESound.TimeUp);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (Global.GAMESTATE != GameState.EndGame)
            {
                if (isBonusTime == true)
                {
                    spriteBatch.DrawString(spriteFont, "+" + bonusTime.ToString() + "s", m_bonusTimePos, new Color(0,255,10), 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.6f);
                }
                int tempGameTime = ((int)m_gameTime);

                if (tempGameTime <= 0)
                    tempGameTime = 0;
                
                int min, sec;
                min = (int)tempGameTime / 60;
                sec = (int)tempGameTime % 60;
                String tempSec = sec.ToString();
                if (sec <= 9)
                    tempSec = "0" + tempSec;
                spriteBatch.DrawString(spriteFont, min.ToString() + ":" + tempSec.ToString(), m_timePos, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.6f);
            }
        }
    }
}
