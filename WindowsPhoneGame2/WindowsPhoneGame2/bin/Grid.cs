using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace WindowsPhoneGame2
{
    public class Grid
    {
        ContentManager m_content;
        int TILE = 80;
        Vector2 EXTRAPOS = new Vector2(50, 200);

        int ROW = 5; //dong
        int COL = 7; //cot

        int tempI, tempJ;
        int[,] m_board = {
	            {5,5,5,5,3},
	            {2,2,1,5,3},
	            {5,2,3,5,4},
	            {1,5,3,5,1},
	            {4,4,1,4,4},
	            {5,3,5,2,4},
	            {1,1,1,2,1}
        };

        public List<Cell> m_listTouch = new List<Cell>();
        public List<Cell> m_listCell = new List<Cell>();
        public List<Cell> m_listNewCell = new List<Cell>();
        public Grid_State m_gridState = Grid_State.Wait;

        GesturesInput gesturesInput;
        SpriteFont gameFont;

        Vector2 m_lastCellPos; // vi tri Cell cuoi cung, dung de bat action di chuyen den vi tri nay sau khi DragComplete
        public int m_lastIndexI, m_lastIndexJ; // tuong tu tren, dung de tao object moi neu drag dc tren 4 cuc
        public Cell_Type m_currentCellType = Cell_Type.None; // loai cua cac cell dang click hien tai, dung de kiem tra cell moi co hop le khong

        public int m_countCell; // tong so cell drag dc, neu drag dc 4 cuc -> tao moi object, k su dung m_listTouch.Count vi doi khi thuc hien truoc m_listTouch.clear()
        public int m_countBeExplosion;
        public int m_countFalling;
        bool canCreate = false;
        public Grid()
        {
            gesturesInput = new GesturesInput();
        }

        public void LoadContent(ContentManager content)
        {
            m_content = content;
            gesturesInput.LoadContent(content);
            gameFont = content.Load<SpriteFont>("SpriteFont1");

            Cell tempCell;
            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    if (m_board[i, j] != 0)
                    {
                        Cell_Type type = (Cell_Type)(m_board[i, j]);
                        tempCell = new Cell(this);
                        tempCell.LoadContent(content, i, j, type);
                        m_listCell.Add(tempCell);
                    }
                }
            }
        }

        public void removeCell(Cell _cell, bool isLast)
        {
            m_countBeExplosion--;
            if (m_gridState == Grid_State.Explosion3 && isLast)
                CreateNew(_cell);
            //else
            if (!isLast)
                CreateNew(_cell);
            
            //if(!isLast || m_gridState == Grid_State.Explosion3)
            //    CreateNew(_cell);
            if ((isLast == true) && (canCreate == true))
            {
                m_listCell.Remove(_cell);
                Create();
                canCreate = false;
            }
            if (isLast == true && m_currentCellType == Cell_Type.Type07)
            {

                m_listCell.Remove(_cell);
                Create();
                canCreate = false;
            }

            else m_listCell.Remove(_cell);
            if (m_gridState == Grid_State.Explosion2 && m_countBeExplosion == 0)
            {
                m_currentCellType = Cell_Type.None;
                m_gridState = Grid_State.Falling;
                Falling();
            }
            if (m_gridState == Grid_State.Explosion3 && m_countBeExplosion == 0)
            {
                m_gridState = Grid_State.Falling;
                m_currentCellType = Cell_Type.None;
                Falling();
            }

        }
        public void CreateNew(Cell _cell)
        {
            Cell _tempCell = new Cell(this);
            int tempIndexI = -1;
            int tempIndexJ = _cell.m_indexJ;
            bool isDone = false;
            while (!isDone)
            {
                isDone = true;
                for (int i = 0; i < m_listNewCell.Count; i++)
                {
                    if (tempIndexI == m_listNewCell[i].m_indexI && tempIndexJ == m_listNewCell[i].m_indexJ)
                    {
                        isDone = false;
                        tempIndexI--;
                    }
                }
            }
            _tempCell.LoadContent(m_content, tempIndexI, tempIndexJ, _cell.m_type);
            m_listNewCell.Add(_tempCell);
        }
        public void Create()
        {
            
            if (m_gridState == Grid_State.Explosion8)
            {
                m_gridState = Grid_State.Falling;
                Falling();
                m_currentCellType = Cell_Type.None;
                return;
            }
            Cell _Cell = new Cell(this);
            if (m_gridState == Grid_State.Explosion3 && m_currentCellType == Cell_Type.Type07)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type08);
                m_listCell.Add(_Cell);
                m_gridState = Grid_State.Falling;
                Falling();
                m_currentCellType = Cell_Type.None;
            }
            if (m_gridState == Grid_State.Explosion4)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type06);
                m_listCell.Add(_Cell);
                m_gridState = Grid_State.Falling;
                Falling();
                m_currentCellType = Cell_Type.None;
            }
            if (m_gridState == Grid_State.Explosion5)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type07);
                m_listCell.Add(_Cell);
                m_gridState = Grid_State.Falling;
                Falling();
                m_currentCellType = Cell_Type.None;
            }
            
        }
        public void Falling()
        {
            for (int i = 0; i < m_listCell.Count; i++)
            {
                int count = 0;
                int _tempI = m_listCell[i].m_indexI;
                int _tempJ = m_listCell[i].m_indexJ;

                for (int m = _tempI; m < COL; m++)
                {
                    if (!isContain(m, _tempJ))
                        count++;
                }
                if (count != 0)
                {
                    Vector2 dir = m_listCell[i].m_position + new Vector2(0, TILE * count);
                    m_listCell[i].m_direction = dir;
                    m_listCell[i].m_state = Cell_State.Falling;
                    m_listCell[i].m_tempIndexI = m_listCell[i].m_indexI + count;
                    m_countFalling++;
                }
            }

            
            for (int i = 0; i < ROW; i++)
            {
                int count = 0;
                int _tempI = 0;
                int _tempJ = i;
                for (int m = _tempI; m < COL; m++)
                {
                    if (!isContain(m, _tempJ))
                        count++;
                }
                if (count != 0)
                {
                    for (int m = 0; m < m_listNewCell.Count; m++)
                    {
                        if (m_listNewCell[m].m_indexJ == _tempJ)
                        {
                            Vector2 dir = m_listNewCell[m].m_position + new Vector2(0, TILE * count);
                            m_listNewCell[m].m_direction = dir;
                            m_listNewCell[m].m_state = Cell_State.Falling;
                            m_listNewCell[m].m_tempIndexI = m_listNewCell[m].m_indexI + count;
                        }
                    }
                    int a = 0;
                    a++;
                }
            }
            m_countFalling += m_listNewCell.Count;
            for (int i = 0; i < m_listNewCell.Count; i++)
            {
                m_listCell.Add(m_listNewCell[i]);
                //m_listNewCell.Remove(m_listNewCell[i]);
            }
            m_listNewCell.Clear();
        }

        public bool isContain(int _indexI, int _indexJ)
        {
            for (int i = 0; i < m_listCell.Count; i++)
            {
                if(m_listCell[i].m_indexI == _indexI && m_listCell[i].m_indexJ == _indexJ)
                    return true;
            }
            return false;
        }
        public void Update(GameTime gameTime)
        {
            gesturesInput.Update(gameTime);
            if ((gesturesInput.GetGestureType() == GestureType.FreeDrag
                || gesturesInput.GetGestureType() == GestureType.HorizontalDrag
                || gesturesInput.GetGestureType() == GestureType.Hold
                || gesturesInput.GetGestureType() == GestureType.VerticalDrag) && (m_gridState == Grid_State.Wait || m_gridState == Grid_State.Touching))
            {
                Vector2 posTap = gesturesInput.GetGesturePosition();
                tempI = (int)(posTap.Y - EXTRAPOS.Y) / TILE;
                tempJ = (int)(posTap.X - EXTRAPOS.X) / TILE;

                for (int i = 0; i < m_listCell.Count; i++)
                {
                    if (m_listCell[i].IsContain(posTap))
                    {
                        if (isAvailable(tempI, tempJ) && (m_currentCellType == m_listCell[i].m_type || m_currentCellType == Cell_Type.None))
                        {
                            m_lastCellPos = m_listCell[i].m_position;
                            m_lastIndexI = m_listCell[i].m_indexI;
                            m_lastIndexJ = m_listCell[i].m_indexJ;
                            if (m_listCell[i].m_state == Cell_State.NoTouch)
                            {
                                m_listCell[i].setBeginTouch();
                                m_listTouch.Add(m_listCell[i]);
                                if (m_currentCellType == Cell_Type.None)
                                {
                                    m_currentCellType = m_listCell[i].m_type;
                                    m_gridState = Grid_State.Touching;
                                }
                            }
                        }
                    }
                }
            }

            if (gesturesInput.GetGestureType() == GestureType.DragComplete)
            {
                switch (m_listTouch.Count)
                {
                    case 0:
                        break;
                    case 1:
                        m_listTouch[0].setEndTouch(false);
                        m_gridState = Grid_State.Wait;
                        m_currentCellType = Cell_Type.None;
                        break;
                    case 2:
                        m_listTouch[0].setEndTouch(true);
                        m_listTouch[0].m_direction = m_listTouch[1].m_position;
                        m_listTouch[1].setEndTouch(true);
                        m_listTouch[1].m_direction = m_listTouch[0].m_position;
                        if (m_gridState == Grid_State.Touching)
                            m_gridState = Grid_State.Explosion2;
                        m_countBeExplosion = m_listTouch.Count;
                        break;
                    default:
                        for (int i = 0; i < m_listTouch.Count; i++)
                        {
                            if (m_listTouch[i].m_indexI == m_lastIndexI && m_listTouch[i].m_indexJ == m_lastIndexJ)
                            {
                                m_listTouch[i].SetWaitAction();
                            }
                            else
                            {
                                m_listTouch[i].setEndTouch(true);
                                m_listTouch[i].m_direction = m_lastCellPos;
                            }
                        }
                        Global.isFly = true;
                        SwitchTouchingTo();
                        m_countBeExplosion = m_listTouch.Count;
                        break;
                }

                m_countCell = m_listTouch.Count;
                
                if (m_countCell >= 4)
                    canCreate = true;
                m_listTouch.Clear();
            }
            if (m_countFalling == 0 && m_gridState == Grid_State.Falling)
                m_gridState = Grid_State.Wait;
            for (int i = 0; i < m_listCell.Count; i++)
            {
                m_listCell[i].Update(gameTime);
            }
        }
        void SwitchTouchingTo()
        {
            if (m_currentCellType == Cell_Type.Type08)
            {
                if (m_gridState == Grid_State.Touching && m_listTouch.Count >= 3)
                {
                    m_gridState = Grid_State.Explosion8;
                    return;
                }
            }
            else
            {
                switch (m_listTouch.Count)
                {
                    case 3:
                        if (m_gridState == Grid_State.Touching)
                            m_gridState = Grid_State.Explosion3;
                        break;
                    case 4:
                        if (m_gridState == Grid_State.Touching)
                            m_gridState = Grid_State.Explosion4;
                        break;
                    default:
                        if (m_gridState == Grid_State.Touching)
                            m_gridState = Grid_State.Explosion5;
                        break;
                }
            }
        }

        bool isAvailable(int _indexI, int _indexJ)
        {
            if (m_currentCellType == Cell_Type.None)
                return true;
            else
            {
                for (int i = 0; i < m_listTouch.Count; i++)
                {
                    if (m_listTouch[i].isAvailable(_indexI, _indexJ))
                        return true;
                }
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Vector2 textPosition = new Vector2(10, 650);
            //spriteBatch.DrawString(gameFont, "GB.FirstCell : " + Global.FirstCell.ToString(), textPosition, Color.White);
            //textPosition = new Vector2(10, 750);

            //spriteBatch.DrawString(gameFont, "Current" + m_currentType.ToString(), textPosition, Color.White);
            //gesturesInput.Draw(spriteBatch);
            //if (m_lastCell != null)
            //{
                //spriteBatch.DrawString(gameFont, "m_lastCellI : " + m_lastCell.m_indexI.ToString(), new Vector2(200, 650), Color.White);
                //spriteBatch.DrawString(gameFont, "m_lastCellJ : " + m_lastCell.m_indexJ.ToString(), new Vector2(200, 750), Color.White);
            //}
            //spriteBatch.DrawString(gameFont, "I : " + tempI.ToString(), new Vector2(10, 650), Color.White);
            //spriteBatch.DrawString(gameFont, "J : " + tempJ.ToString(), new Vector2(10, 750), Color.White);
            for (int i = 0; i < m_listNewCell.Count; i++)
            {
                m_listNewCell[i].Draw(spriteBatch);
            }
            for (int i = 0; i < m_listCell.Count; i++)
            {
                m_listCell[i].Draw(spriteBatch);
            }
        }
    }
}
