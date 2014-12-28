using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame2
{
    public class PointManager
    {
        int TYPE1_BONUSPOINT = 150;
        int TYPE1_STARTPOINT = 100;

        int TYPE2_BONUSPOINT = 150;
        int TYPE2_STARTPOINT = 100;

        int TYPE3_BONUSPOINT = 150;
        int TYPE3_STARTPOINT = 100;

        int TYPE4_BONUSPOINT = 150;
        int TYPE4_STARTPOINT = 100;

        int TYPE5_BONUSPOINT = 150;
        int TYPE5_STARTPOINT = 100;

        int TYPE6_BONUSPOINT = 300;
        int TYPE6_STARTPOINT = 500;

        int TYPE7_BONUSPOINT = 400;
        int TYPE7_STARTPOINT = 850;

        int TYPE8_BONUSPOINT = 600;
        int TYPE8_STARTPOINT = 1500;

        float FLYTIME = 0.5f;
        Vector2 ACCLOC = new Vector2(0.0f,-0.07f);

        bool m_isDrawPoint = false;
        int m_currentPoint = 0;
        public int m_totalPoint = 0;

        float m_timeFly = 0;
        Vector2 m_posPoint = Vector2.Zero;
        Sprite m_pHighScore;

        public PointManager()
        {
            m_pHighScore = new Sprite(ResourceManager.GetInstance().GetSprite("High_Score"));
            m_pHighScore.Depth = 0.6f;
        }
        public void Reset()
        {
            m_totalPoint = 0;
            m_currentPoint = 0;
            m_isDrawPoint = false;
        }
        public void SetPoint(Cell_Type _cellType, int _countCell, Vector2 _posPoint)
        {
            m_isDrawPoint = true;
            m_posPoint = _posPoint + new Vector2(5, 10) ;
            m_timeFly = 0;
            if (_countCell == 2)
            {
                m_currentPoint = - 100;
                m_totalPoint += m_currentPoint;
                return;
            }

            int _iPoint = 0;
            int _typeBonusPoint = 0;
            int _startPoint = 0;
            int _numBonusCell = _countCell - 3;

            switch (_cellType)
            {
                case Cell_Type.Type01:
                    _typeBonusPoint = TYPE1_BONUSPOINT;
                    _startPoint = TYPE1_STARTPOINT;
                    break;
                case Cell_Type.Type02:
                    _typeBonusPoint = TYPE2_BONUSPOINT;
                    _startPoint = TYPE2_STARTPOINT;
                    break;
                case Cell_Type.Type03:
                    _typeBonusPoint = TYPE3_BONUSPOINT;
                    _startPoint = TYPE3_STARTPOINT;
                    break;
                case Cell_Type.Type04:
                    _typeBonusPoint = TYPE4_BONUSPOINT;
                    _startPoint = TYPE4_STARTPOINT;
                    break;
                case Cell_Type.Type05:
                    _typeBonusPoint = TYPE5_BONUSPOINT;
                    _startPoint = TYPE5_STARTPOINT;
                    break;
                case Cell_Type.Type06:
                    _typeBonusPoint = TYPE6_BONUSPOINT;
                    _startPoint = TYPE6_STARTPOINT;
                    break;
                case Cell_Type.Type07:
                    _typeBonusPoint = TYPE7_BONUSPOINT;
                    _startPoint = TYPE7_STARTPOINT;
                    break;
                case Cell_Type.Type08:
                    _typeBonusPoint = TYPE8_BONUSPOINT;
                    _startPoint = TYPE8_STARTPOINT;
                    break;
            }
            _iPoint = _startPoint + _typeBonusPoint * _numBonusCell;
            m_currentPoint = _iPoint;
            m_totalPoint += m_currentPoint;
            return;
        }

        public void Update(GameTime gameTime)
        {
            if (m_isDrawPoint)
            {
                m_posPoint += ACCLOC * gameTime.ElapsedGameTime.Milliseconds;
                m_timeFly += (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                if (m_timeFly >= FLYTIME)
                {
                    m_isDrawPoint = false;
                }
            }
            if (Global.GAMEMODE == GameMode.EasyClassic)
            {
                if (m_totalPoint >= DataManager.GetInstance().EasyHighScore)
                    DataManager.GetInstance().EasyHighScore = m_totalPoint;
            }
            if (Global.GAMEMODE == GameMode.NormalClassic)
            {
                if (m_totalPoint >= DataManager.GetInstance().NormalHighScore)
                    DataManager.GetInstance().NormalHighScore = m_totalPoint;
            }
            
        }
        public void Draw(SpriteBatch spriteBatch,SpriteFont spriteFont)
        {
            if (m_isDrawPoint)
            {
                string tempPoint = m_currentPoint.ToString();
                if (m_currentPoint >= 0)
                    tempPoint = "+" + tempPoint;
                spriteBatch.DrawString(spriteFont, tempPoint, m_posPoint, Color.Yellow, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.6f);
            }

            if (Global.GAMESTATE != GameState.EndGame)
            {
                spriteBatch.DrawString(spriteFont, m_totalPoint.ToString(), new Vector2(180, 18), Color.White, 0.0f, Vector2.Zero, 0.68f, SpriteEffects.None, 0.6f);
                //chinese
                //spriteBatch.DrawString(spriteFont, m_totalPoint.ToString(), new Vector2(155, 18), Color.White, 0.0f, Vector2.Zero, 0.68f, SpriteEffects.None, 0.6f);
                
                if (Global.GAMEMODE == GameMode.EasyClassic)
                {
                    m_pHighScore.Draw(spriteBatch, new Vector2(50, 60));
                    spriteBatch.DrawString(spriteFont, DataManager.GetInstance().EasyHighScore.ToString(), new Vector2(155, 65), new Color(0, 227, 254), 0.0f, Vector2.Zero, 0.74f, SpriteEffects.None, 0.6f);
                    //chinese
                    //m_pHighScore.Draw(spriteBatch, new Vector2(50, 70));
                    //spriteBatch.DrawString(spriteFont, DataManager.GetInstance().EasyHighScore.ToString(), new Vector2(180, 70), Color.White, 0.0f, Vector2.Zero, 0.74f, SpriteEffects.None, 0.6f);
                }
                if (Global.GAMEMODE == GameMode.NormalClassic)
                {
                    m_pHighScore.Draw(spriteBatch, new Vector2(50, 60));
                    spriteBatch.DrawString(spriteFont, DataManager.GetInstance().NormalHighScore.ToString(), new Vector2(155, 65), new Color(0, 227, 254), 0.0f, Vector2.Zero, 0.74f, SpriteEffects.None, 0.6f);
                    //chinese
                    //m_pHighScore.Draw(spriteBatch, new Vector2(50, 70));
                    //spriteBatch.DrawString(spriteFont, DataManager.GetInstance().NormalHighScore.ToString(), new Vector2(180, 70), Color.White, 0.0f, Vector2.Zero, 0.68f, SpriteEffects.None, 0.6f);
                }
            }
        }
    }
}
