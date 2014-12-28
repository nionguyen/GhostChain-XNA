using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsPhoneGame2
{
    public class LevelManager
    {
        public int m_currentLevel;

        Sprite m_pType1;
        Sprite m_pType2;
        Sprite m_pType3;
        Sprite m_pType4;
        Sprite m_pType5;
        Sprite m_pType6;
        Sprite m_pType7;
        Sprite m_pType8;
        Sprite m_pStar;
        

        Vector2 m_pos1, m_pos2, m_pos3;
        Vector2 m_posFont1, m_posFont2, m_posFont3;
        Vector2 m_goalStringPos;

        int Goal_Type1 = 0;
        int Goal_Type2 = 0;
        int Goal_Type3 = 0;
        int Goal_Type4 = 0;
        int Goal_Type5 = 0;
        int Goal_Type6 = 0;
        int Goal_Type7 = 0;
        int Goal_Type8 = 0;

        public int MAXLEVEL = 24;
        public LevelManager()
        {//45 65 - 124 65 - 206 65
            m_pos1 = new Vector2(40, 65);
            m_pos2 = new Vector2(124, 65);
            m_pos3 = new Vector2(203, 65);
            m_posFont1 = new Vector2(85, 75);
            m_posFont2 = new Vector2(163, 75);
            m_posFont3 = new Vector2(235, 75);
            m_goalStringPos = new Vector2(72, 20);
            Sprite m_pTempType1 = ResourceManager.GetInstance().GetSprite("Cell/Type01_NoTouch");
            Sprite m_pTempType2 = ResourceManager.GetInstance().GetSprite("Cell/Type02_NoTouch");
            Sprite m_pTempType3 = ResourceManager.GetInstance().GetSprite("Cell/Type03_NoTouch");
            Sprite m_pTempType4 = ResourceManager.GetInstance().GetSprite("Cell/Type04_NoTouch");
            Sprite m_pTempType5 = ResourceManager.GetInstance().GetSprite("Cell/Type05_NoTouch");
            Sprite m_pTempType6 = ResourceManager.GetInstance().GetSprite("Cell/Type06_NoTouch");
            Sprite m_pTempType7 = ResourceManager.GetInstance().GetSprite("Cell/Type07_NoTouch");
            Sprite m_pTempType8 = ResourceManager.GetInstance().GetSprite("Cell/Type08_NoTouch");
            
            m_pType1 = new Sprite(m_pTempType1);
            m_pType1.Scale = 0.6f;
            m_pType1.Depth = 0.6f;
            m_pType2 = new Sprite(m_pTempType2);
            m_pType2.Scale = 0.6f;
            m_pType2.Depth = 0.6f;
            m_pType3 = new Sprite(m_pTempType3);
            m_pType3.Scale = 0.6f;
            m_pType3.Depth = 0.6f;
            m_pType4 = new Sprite(m_pTempType4);
            m_pType4.Scale = 0.6f;
            m_pType4.Depth = 0.6f;
            m_pType5 = new Sprite(m_pTempType5);
            m_pType5.Scale = 0.6f;
            m_pType5.Depth = 0.6f;
            m_pType6 = new Sprite(m_pTempType6);
            m_pType6.Scale = 0.6f;
            m_pType6.Depth = 0.6f;
            m_pType7 = new Sprite(m_pTempType7);
            m_pType7.Scale = 0.6f;
            m_pType7.Depth = 0.6f;
            m_pType8 = new Sprite(m_pTempType8);
            m_pType8.Scale = 0.6f;
            m_pType8.Depth = 0.6f;
            m_pStar = ResourceManager.GetInstance().GetSprite("Star");
            m_pStar.Depth = 0.6f;
        }

        public bool isWin()
        {
            if (Goal_Type1 <= 0
                && Goal_Type2 <= 0
                && Goal_Type3 <= 0
                && Goal_Type4 <= 0
                && Goal_Type5 <= 0
                && Goal_Type6 <= 0
                && Goal_Type7 <= 0
                && Goal_Type8 <= 0)
                return true;
            return false;
        }

        public void Reset()
        {
            if (Global.GAMEMODE == GameMode.Level)
                SetLevel(m_currentLevel);
        }

        public void SetLevel(int level)
        {
            m_currentLevel = level;
            switch (level)
            {
                case 1:
                    SetZero();
                    SetLevel_01();
                    break;
                case 2:
                    SetZero();
                    SetLevel_02();
                    break;
                case 3:
                    SetZero();
                    SetLevel_03();
                    break;
                case 4:
                    SetZero();
                    SetLevel_04();
                    break;
                case 5:
                    SetZero();
                    SetLevel_05();
                    break;
                case 6:
                    SetZero();
                    SetLevel_06();
                    break;
                case 7:
                    SetZero();
                    SetLevel_07();
                    break;
                case 8:
                    SetZero();
                    SetLevel_08();
                    break;
                case 9:
                    SetZero();
                    SetLevel_09();
                    break;
                case 10:
                    SetZero();
                    SetLevel_10();
                    break;
                case 11:
                    SetZero();
                    SetLevel_11();
                    break;
                case 12:
                    SetZero();
                    SetLevel_12();
                    break;
                case 13:
                    SetZero();
                    SetLevel_13();
                    break;
                case 14:
                    SetZero();
                    SetLevel_14();
                    break;
                case 15:
                    SetZero();
                    SetLevel_15();
                    break;
                case 16:
                    SetZero();
                    SetLevel_16();
                    break;
                case 17:
                    SetZero();
                    SetLevel_17();
                    break;
                case 18:
                    SetZero();
                    SetLevel_18();
                    break;
                case 19:
                    SetZero();
                    SetLevel_19();
                    break;
                case 20:
                    SetZero();
                    SetLevel_20();
                    break;
                case 21:
                    SetZero();
                    SetLevel_21();
                    break;
                case 22:
                    SetZero();
                    SetLevel_22();
                    break;
                case 23:
                    SetZero();
                    SetLevel_23();
                    break;
                case 24:
                    SetZero();
                    SetLevel_24();
                    break;
                default:
                    SetZero();
                    SetLevel_24();
                    break;
            }
        }
        public void SetGoal(Cell_Type _type, int _countCell)
        {
            switch (_type)
            {
                case Cell_Type.Type01:
                    Goal_Type1 -= _countCell;
                    break;
                case Cell_Type.Type02:
                    Goal_Type2 -= _countCell;
                    break;
                case Cell_Type.Type03:
                    Goal_Type3 -= _countCell;
                    break;
                case Cell_Type.Type04:
                    Goal_Type4 -= _countCell;
                    break;
                case Cell_Type.Type05:
                    Goal_Type5 -= _countCell;
                    break;
                case Cell_Type.Type06:
                    Goal_Type6 -= _countCell;
                    break;
                case Cell_Type.Type07:
                    Goal_Type7 -= _countCell;
                    break;
                case Cell_Type.Type08:
                    Goal_Type8 -= _countCell;
                    break;
            }
        }
        private void SetZero()
        {
            Goal_Type1 = 0;
            Goal_Type2 = 0;
            Goal_Type3 = 0;
            Goal_Type4 = 0;
            Goal_Type5 = 0;
            Goal_Type6 = 0;
            Goal_Type7 = 0;
            Goal_Type8 = 0;
        }


        public void NewLevel()
        {
            int newLevel = m_currentLevel + 1;
            if (newLevel > Global.MAXLEVEL + 1)
            {
                newLevel = Global.MAXLEVEL + 1;
            }
            if (newLevel > DataManager.GetInstance().CurrentLevel)
                DataManager.GetInstance().CurrentLevel = newLevel;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (Global.GAMESTATE == GameState.EndGame || Global.GAMESTATE == GameState.PauseGame)
                return;
            spriteBatch.DrawString(spriteFont, "Lv " + m_currentLevel.ToString(), new Vector2(325, 12), Color.White, 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0.6f);
            int tempType1 = Goal_Type1;
            int tempType2 = Goal_Type2;
            int tempType3 = Goal_Type3;
            int tempType4 = Goal_Type4;
            int tempType5 = Goal_Type5;
            int tempType6 = Goal_Type6;
            int tempType7 = Goal_Type7;
            int tempType8 = Goal_Type8;
            if (tempType1 <= 0)
                tempType1 = 0;
            if (tempType2 <= 0)
                tempType2 = 0;
            if (tempType3 <= 0)
                tempType3 = 0;
            if (tempType4 <= 0)
                tempType4 = 0;
            if (tempType5 <= 0)
                tempType5 = 0;
            if (tempType6 <= 0)
                tempType6 = 0;
            if (tempType7 <= 0)
                tempType7 = 0;
            if (tempType8 <= 0)
                tempType8 = 0;


            if (m_currentLevel == 1)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType2.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType1, tempType2, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 2)
            {
                m_pType2.Draw(spriteBatch, m_pos1);
                m_pType4.Draw(spriteBatch, m_pos2);
                m_pType5.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType2, tempType4, tempType5, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 3)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType4.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType3, tempType4, spriteBatch, spriteFont);
            }

            if (m_currentLevel == 4)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType5.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType1, tempType5, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 5)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType4.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType3, tempType4, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 6)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType6.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType3, tempType6, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 7)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType6.Draw(spriteBatch, m_pos2);
                m_pType7.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType1, tempType6, tempType7, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 8)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType8.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType3, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 9)
            {
                m_pType4.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType4, tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 10)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType6.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType1, tempType6, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 11)
            {
                m_pType6.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType6, tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 12)
            {
                m_pType8.Draw(spriteBatch, m_pos1);

                DrawStringGoal(tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 13)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType1, tempType7, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 14)
            {
                m_pType2.Draw(spriteBatch, m_pos1);
                m_pType6.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType2, tempType6, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 15)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType8.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType3, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 16)
            {
                m_pType4.Draw(spriteBatch, m_pos1);
                m_pType5.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType4, tempType5, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 17)
            {
                m_pType3.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType3, tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 18)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType2.Draw(spriteBatch, m_pos2);
                m_pType7.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType1, tempType2, tempType7, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 19)
            {
                m_pType6.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType6, tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 20)
            {
                m_pType7.Draw(spriteBatch, m_pos1);
                m_pType8.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 21)
            {
                m_pType2.Draw(spriteBatch, m_pos1);
                m_pType3.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType2, tempType3, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 22)
            {
                m_pType8.Draw(spriteBatch, m_pos1);

                DrawStringGoal(tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 23)
            {
                m_pType6.Draw(spriteBatch, m_pos1);
                m_pType7.Draw(spriteBatch, m_pos2);
                m_pType8.Draw(spriteBatch, m_pos3);

                DrawStringGoal(tempType6, tempType7, tempType8, spriteBatch, spriteFont);
            }
            if (m_currentLevel == 24)
            {
                m_pType1.Draw(spriteBatch, m_pos1);
                m_pType8.Draw(spriteBatch, m_pos2);

                DrawStringGoal(tempType1, tempType8, spriteBatch, spriteFont);
            }

        }

        void DrawStringGoal(int _goal1, int _goal2, int _goal3, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (_goal1 > 0)
            {
                if (_goal1 >= 10)
                    spriteBatch.DrawString(spriteFont, "x" + _goal1.ToString(), m_posFont1, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.DrawString(spriteFont, "x0" + _goal1.ToString(), m_posFont1, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont1 + new Vector2(10, 0));
            }
            if (_goal2 > 0)
            {
                if (_goal2 >= 10)
                    spriteBatch.DrawString(spriteFont, "x" + _goal2.ToString(), m_posFont2, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.DrawString(spriteFont, "x0" + _goal2.ToString(), m_posFont2, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont2 + new Vector2(10, 0));
            }
            if (_goal3 > 0)
            {
                if (_goal3 >= 10)
                    spriteBatch.DrawString(spriteFont, "x" + _goal3.ToString(), m_posFont3, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.DrawString(spriteFont, "x0" + _goal3.ToString(), m_posFont3, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont3 + new Vector2(10, 0));
            }
        }

        void DrawStringGoal(int _goal1, int _goal2, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (_goal1 > 0)
            {
                if(_goal1 >= 10)
                    spriteBatch.DrawString(spriteFont, "x" + _goal1.ToString(), m_posFont1, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.DrawString(spriteFont, "x0" + _goal1.ToString(), m_posFont1, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont1 + new Vector2(10, 0));
            }
            if (_goal2 > 0)
            {
                if (_goal2 >= 10)
                    spriteBatch.DrawString(spriteFont, "x" + _goal2.ToString(), m_posFont2, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
                else
                    spriteBatch.DrawString(spriteFont, "x0" + _goal2.ToString(), m_posFont2, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont2 + new Vector2(10, 0));
            }
        }

        void DrawStringGoal(int _goal1, SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (_goal1 > 0)
            {
                spriteBatch.DrawString(spriteFont, "x0" + _goal1.ToString(), m_posFont1, Color.White, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.6f);
            }
            else
            {
                m_pStar.Draw(spriteBatch, m_posFont1 + new Vector2(5, 0));
            }
        }

        private void SetLevel_01()
        {
            Goal_Type1 = 8;
            Goal_Type2 = 8;
        }

        private void SetLevel_02()
        {
            Goal_Type2 = 8;
            Goal_Type4 = 8;
            Goal_Type5 = 8;
        }

        private void SetLevel_03()
        {
            Goal_Type3 = 15;
            Goal_Type4 = 15;
        }

        private void SetLevel_04()
        {
            Goal_Type1 = 20;
            Goal_Type5 = 20;
        }

        private void SetLevel_05()
        {
            Goal_Type3 = 30;
            Goal_Type4 = 30;
        }
        private void SetLevel_06()
        {
            Goal_Type3 = 20;
            Goal_Type6 = 6;
        }
        private void SetLevel_07()
        {
            Goal_Type1 = 20;
            Goal_Type6 = 10;
            Goal_Type7 = 5;
        }
        private void SetLevel_08()
        {
            Goal_Type3 = 25;
            Goal_Type8 = 5;
        }
        private void SetLevel_09()
        {
            Goal_Type3 = 25;
            Goal_Type8 = 5;
        }
        private void SetLevel_10()
        {
            Goal_Type4 = 25;
            Goal_Type7 = 10;
            Goal_Type8 = 5;
        }
        private void SetLevel_11()
        {
            Goal_Type6 = 15;
            Goal_Type7 = 15;
            Goal_Type8 = 10;
        }
        private void SetLevel_12()
        {
            Goal_Type8 = 20;
        }
        private void SetLevel_13()
        {
            Goal_Type1 = 30;
            Goal_Type7 = 20;
        }
        private void SetLevel_14()
        {
            Goal_Type2 = 40;
            Goal_Type6 = 10;
            Goal_Type8 = 10;
        }
        private void SetLevel_15()
        {
            Goal_Type3 = 60;
            Goal_Type8 = 15;
        }

        private void SetLevel_16()
        {
            Goal_Type4 = 60;
            Goal_Type5 = 60;
        }
        private void SetLevel_17()
        {
            Goal_Type3 = 60;
            Goal_Type7 = 15;
            Goal_Type8 = 20;
        }
        private void SetLevel_18()
        {
            Goal_Type1 = 60;
            Goal_Type2 = 60;
            Goal_Type7 = 20;
        }
        private void SetLevel_19()
        {
            Goal_Type6 = 20;
            Goal_Type7 = 20;
            Goal_Type8 = 10;
        }
        private void SetLevel_20()
        {
            Goal_Type7 = 25;
            Goal_Type8 = 20;
        }
        private void SetLevel_21()
        {
            Goal_Type2 = 60;
            Goal_Type3 = 50;
            Goal_Type8 = 20;
        }
        private void SetLevel_22()
        {
            Goal_Type8 = 30;
        }
        private void SetLevel_23()
        {
            Goal_Type6 = 25;
            Goal_Type7 = 25;
            Goal_Type8 = 15;
        }
        private void SetLevel_24()
        {
            Goal_Type1 = 40;
            Goal_Type8 = 30;
        }
    }
}