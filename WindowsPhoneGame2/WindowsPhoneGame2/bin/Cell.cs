using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsPhoneGame2
{
    public class Cell
    {
        int TILE = 80;
        Vector2 EXTRAPOS = new Vector2(50, 200);

        Vector2 EXTRAPOS_EXPLOSION = new Vector2(85, 85);
        
        public Cell_Type m_type;
        public Cell_State m_state = Cell_State.NoTouch;
        bool isLastCell = false;
        
        Sprite m_pSprite;
        Sprite m_pNoTouch;
        Sprite m_pTouch;
        Sprite m_pAction;
        Sprite m_pExplosion;
        
        public Vector2 m_position;
        public int m_indexI, m_indexJ;
        public int m_tempIndexI;
        Rectangle rect;

        public Vector2 m_direction;

        Grid m_grid;

        public Cell(Grid _grid)
        {
            m_grid = _grid;
        }

        public void LoadContent(ContentManager content, int _indexI, int _indexJ, Cell_Type _type)
        {
            m_type = _type;
            m_indexI = _indexI;
            m_indexJ = _indexJ;

            m_position = new Vector2(m_indexJ * TILE + EXTRAPOS.X, m_indexI * TILE + EXTRAPOS.Y);
            rect = new Rectangle((int)m_position.X, (int)m_position.Y, TILE, TILE);
            InitSprite();
        }


        private void InitSprite()
        {
            
            switch (m_type)
            {
                case Cell_Type.Type01:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type01_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type01_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type01_Action"));
                    break;
                case Cell_Type.Type02:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type02_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type02_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type02_Action"));
                    break;
                case Cell_Type.Type03:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type03_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type03_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type03_Action"));
                    break;
                case Cell_Type.Type04:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type04_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type04_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type04_Action"));
                    break;
                case Cell_Type.Type05:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type05_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type05_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type05_Action"));
                    break;
                case Cell_Type.Type06:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type06_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type06_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type06_Action"));
                    break;
                case Cell_Type.Type07:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type07_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type07_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type07_Action"));
                    break;
                case Cell_Type.Type08:
                    m_pNoTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type08_NoTouch"));
                    m_pTouch = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type08_Touching"));
                    m_pAction = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Type08_Action"));
                    break;
                
            }
            m_pExplosion = new Sprite(ResourceManager.GetInstance().GetSprite("Cell/Explosion"));
            m_pExplosion.Depth = 0.5f;
            m_pSprite = m_pNoTouch;
        }

        public bool IsContain(Vector2 posTap)
        {
            Point tap = new Point((int)posTap.X, (int)posTap.Y);
            return rect.Contains(tap);
        }

        public void setBeginTouch()
        {
            if (m_state == Cell_State.NoTouch)
            {
                m_state = Cell_State.Touching;
                m_pSprite = m_pTouch;
            }
        }

        public void setEndTouch(bool isEndTouch)
        {
            if (isEndTouch == false)
            {
                m_state = Cell_State.NoTouch;
                m_pSprite = m_pNoTouch;
            }
            if (m_state == Cell_State.Touching)
            {
                m_state = Cell_State.Action;
                m_pSprite = m_pAction;
            }
        }
        public void SetWaitAction()
        {
            m_state = Cell_State.WaitAction;
            m_pSprite = m_pAction;
            isLastCell = true;
            //isFly = false;
        }
        public bool isAvailable(int _indexI, int _indexJ)
        {
            int tempI,tempJ;

            tempI = _indexI + 1;
            tempJ = _indexJ;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI - 1;
            tempJ = _indexJ;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI;
            tempJ = _indexJ + 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI;
            tempJ = _indexJ - 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI - 1;
            tempJ = _indexJ - 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI + 1;
            tempJ = _indexJ + 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI + 1;
            tempJ = _indexJ - 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            tempI = _indexI - 1;
            tempJ = _indexJ + 1;
            if (m_indexI == tempI && m_indexJ == tempJ)
                return true;

            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (m_state == Cell_State.Die)
            {
                if(isLastCell == false)
                    m_grid.removeCell(this,false);
                else 
                    m_grid.removeCell(this, true);
                return;
            }
            Vector2 _vectorDir = m_direction - m_position;
            if (m_state == Cell_State.Action)//if (m_isFly)
            {
                m_position += _vectorDir * 13 / gameTime.ElapsedGameTime.Milliseconds;
                if (Math.Abs(_vectorDir.X) < 1 && Math.Abs(_vectorDir.Y) < 1)
                {
                    Global.isFly = false;
                    m_position = m_direction;
                    m_state = Cell_State.Die;
                    //m_pSprite = m_pExplosion;
                }
            }

            if (m_state == Cell_State.Falling)//if (m_isFly)
            {
                m_position += _vectorDir * 25 / gameTime.ElapsedGameTime.Milliseconds;
                if (Math.Abs(_vectorDir.X) < 1 && Math.Abs(_vectorDir.Y) < 1)
                {
                    Global.isFly = false;
                    m_position = m_direction;
                    m_indexI = m_tempIndexI;
                    m_tempIndexI = 0;
                    m_state = Cell_State.NoTouch;
                    isLastCell = false;
                    
                    rect = new Rectangle((int)m_position.X, (int)m_position.Y, TILE, TILE);
                    //m_pSprite = m_pExplosion;
                    m_grid.m_countFalling--;
                }
            }

            if (m_state == Cell_State.WaitAction && Global.isFly == false)
            {
                m_state = Cell_State.Explosion;
                m_pSprite = m_pExplosion;
                m_position -= EXTRAPOS_EXPLOSION;
            }
            if (m_state == Cell_State.Explosion)
            {
                //if (m_pSprite.Loop >= 1)
                if(m_pSprite.CurentFrame == m_pSprite.LastFrame)
                    m_state = Cell_State.Die;
            }        

            if(m_state != Cell_State.Action)
                m_pSprite.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(m_state != Cell_State.Die)
                m_pSprite.Draw(spriteBatch, m_position);
        }


    }
}
