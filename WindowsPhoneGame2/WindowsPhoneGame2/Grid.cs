using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame2
{
    public class Grid
    {

        MainGameScreen m_mainGame;
        ContentManager m_content;
        int TILE = 78;
        Vector2 EXTRAPOS = new Vector2(6, 154);

        int ROW = 6; //dong
        int COL = 8; //cot

        

        int[,] m_board = {
	            {1,1,1,1,1,1},
                {1,1,1,1,1,1},
                {1,1,1,1,1,1},
                {1,1,1,1,1,1},
                {1,1,1,1,1,1},
                {1,1,1,1,1,1},
                {1,1,1,1,1,1},
	            {1,1,1,1,1,1}
        };
        

        public List<Cell> m_listTouch = new List<Cell>();
        public List<Cell> m_listCell = new List<Cell>();
        public List<Cell> m_listNewCell = new List<Cell>();
        List<Cell_Type> m_listRandomType = new List<Cell_Type>();
        int m_currentRandomType = 0;
        public Grid_State m_gridState = Grid_State.Wait;

        SpriteFont gameFont;

        Vector2 m_lastCellPos; // vi tri Cell cuoi cung, dung de bat action di chuyen den vi tri nay sau khi DragComplete
        public int m_lastIndexI, m_lastIndexJ; // tuong tu tren, dung de tao object moi neu drag dc tren 4 cuc
        public Cell_Type m_currentCellType = Cell_Type.None; // loai cua cac cell dang click hien tai, dung de kiem tra cell moi co hop le khong

        public int m_countCell; // tong so cell drag dc, neu drag dc 4 cuc -> tao moi object, k su dung m_listTouch.Count vi doi khi thuc hien truoc m_listTouch.clear()
        public int m_countBeExplosion;
        public int m_countFalling;

        Random random = new Random();
        //bool canCreate = false;
        public Grid()
        {
        }

        void randomCell(int countCell)
        {
            m_listRandomType.Clear();
            m_currentRandomType = 0;
            int _countCell = countCell;
            int rNumber = 0;

            if (Global.GAMEMODE == GameMode.NormalClassic || Global.GAMEMODE == GameMode.Level)
            {
                while (_countCell > 0)
                {
                    rNumber = random.Next(1, 6);
                    m_listRandomType.Add((Cell_Type)rNumber);
                    _countCell--;
                }
                return;
            }
            if (Global.GAMEMODE == GameMode.EasyClassic)
            {
                rNumber = random.Next(1, 5);
                List<Cell_Type> m_newlistCellType = new List<Cell_Type>();
                if (rNumber != (int)Cell_Type.Type01)
                    m_newlistCellType.Add(Cell_Type.Type01);
                if (rNumber != (int)Cell_Type.Type02)
                    m_newlistCellType.Add(Cell_Type.Type02);
                if (rNumber != (int)Cell_Type.Type03)
                    m_newlistCellType.Add(Cell_Type.Type03);
                if (rNumber != (int)Cell_Type.Type04)
                    m_newlistCellType.Add(Cell_Type.Type04);
                if (rNumber != (int)Cell_Type.Type05)
                    m_newlistCellType.Add(Cell_Type.Type05);
                while (_countCell > 0)
                {
                    int randomNumber = random.Next(1, 100);
                    if (randomNumber >= 1 && randomNumber <= 33)
                    {
                        m_listRandomType.Add(m_newlistCellType[0]);
                    }
                    if (randomNumber > 33 && randomNumber <= 66)
                    {
                        m_listRandomType.Add(m_newlistCellType[1]);
                    }
                    if (randomNumber > 66 && randomNumber <= 100)
                    {
                        m_listRandomType.Add(m_newlistCellType[2]);
                    }
                    _countCell--;
                }
                return;
            }
        }

        public void LoadContent(ContentManager content, GameScreen _mainGame)
        {
            m_mainGame = (MainGameScreen)_mainGame;
            m_content = content;
            gameFont = content.Load<SpriteFont>("Font/GameFont");
            m_gridState = Grid_State.Falling;
            Cell tempCell;
            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    if (m_board[i, j] == 1)
                    {
                        int randomNumber = 0;
                        if(Global.GAMEMODE == GameMode.EasyClassic)
                            randomNumber = random.Next(1, 5);
                        if(Global.GAMEMODE == GameMode.NormalClassic || Global.GAMEMODE == GameMode.Level)
                            randomNumber = random.Next(1, 6);
                        Cell_Type type = (Cell_Type)randomNumber;
                        tempCell = new Cell(this);
                        tempCell.LoadContent(content, i - COL, j, type);
                        m_listNewCell.Add(tempCell);
                    }
                }
            }
            Falling();
        }

        public void Reset(ContentManager content)
        {
            m_listTouch.Clear();
            m_listCell.Clear();
            m_listNewCell.Clear();
            m_listRandomType.Clear();
            m_currentRandomType = 0;
            m_gridState = Grid_State.Falling;
            m_lastCellPos = Vector2.Zero;
            m_lastIndexI = 0;
            m_lastIndexJ = 0;
            m_currentCellType = Cell_Type.None;
            m_countCell = 0;
            m_countBeExplosion = 0;
            m_countFalling = 0;

            Cell tempCell;
            for (int i = 0; i < COL; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                    if (m_board[i, j] == 1)
                    {
                        int randomNumber = 0;
                        if (Global.GAMEMODE == GameMode.EasyClassic)
                            randomNumber = random.Next(1, 5);
                        if (Global.GAMEMODE == GameMode.NormalClassic || Global.GAMEMODE == GameMode.Level)
                            randomNumber = random.Next(1, 6);
                        Cell_Type type = (Cell_Type)randomNumber;
                        tempCell = new Cell(this);
                        tempCell.LoadContent(content, i - COL, j, type);
                        m_listNewCell.Add(tempCell);
                    }
                }
            }
            Falling();
        }
        public void removeCell(Cell _cell, bool isLast)
        {
            if (!isLast)
            {
                if(m_countCell >= 3)
                    SoundManager.GetInstance().PlaySound(ESound.Fly);
                
            }
            else
                SoundManager.GetInstance().PlaySound(ESound.CompleteCell);
                
            m_countBeExplosion--;

            if (!isLast)
                CreateNew(_cell);
            m_listCell.Remove(_cell);
            if (isLast)
            {
                m_mainGame.SetGoal(m_currentCellType, m_countCell);
                if (m_countCell >= 3)
                {
                    m_mainGame.SetPoint(m_currentCellType, m_countCell, m_lastCellPos);
                    m_mainGame.SetTime(m_currentCellType, m_countCell);
                }
            }

            if (m_gridState == Grid_State.ToTwo)
            {
                //m_mainGame.SetPoint(m_currentCellType, m_countCell, m_lastCellPos);
            }

            if (m_gridState == Grid_State.ToThree)
            {
                if (isLast)
                {
                    CreateNew(_cell);
                }

            }
            if (m_gridState == Grid_State.ToFour)
            {
                if (isLast)
                {
                    Create();
                }

            }
            if (m_gridState == Grid_State.ToFive)
            {
                if (isLast)
                {
                    Create();
                }
            }
            if (m_gridState == Grid_State.ToGold)
            {
                if (isLast)
                {
                    Create();
                }
            }
            if (m_gridState == Grid_State.ToFinal)
            {
                if (isLast)
                {
                    CreateNew(_cell);
                }
            }

            if (m_countBeExplosion == 0)
            {
                m_gridState = Grid_State.Falling;
                Falling();
                m_currentCellType = Cell_Type.None;
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

            _tempCell.LoadContent(m_content, tempIndexI, tempIndexJ, m_listRandomType[m_currentRandomType]);
            m_currentRandomType++;
            m_listNewCell.Add(_tempCell);
        }
        public void Create()
        {
            Cell _Cell = new Cell(this);
            if (m_gridState == Grid_State.ToFour)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type06);
                m_listCell.Add(_Cell);
            }
            if (m_gridState == Grid_State.ToFive)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type07);
                m_listCell.Add(_Cell);
            }
            if (m_gridState == Grid_State.ToGold)
            {
                _Cell.LoadContent(m_content, m_lastIndexI, m_lastIndexJ, Cell_Type.Type08);
                m_listCell.Add(_Cell);
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
                }
            }
            m_countFalling += m_listNewCell.Count;
            for (int i = 0; i < m_listNewCell.Count; i++)
            {
                m_listCell.Add(m_listNewCell[i]);
            }
            m_listNewCell.Clear();
        }

        public bool isContain(int _indexI, int _indexJ)
        {
            for (int i = 0; i < m_listCell.Count; i++)
            {
                if (m_listCell[i].m_indexI == _indexI && m_listCell[i].m_indexJ == _indexJ)
                    return true;
            }
            return false;
        }
        public void Update(GameTime gameTime)
        {
            if (Global.GAMESTATE == GameState.Running)
                InputHandle(gameTime);
            for (int i = 0; i < m_listCell.Count; i++)
            {
                m_listCell[i].Update(gameTime);
            }
        }

        void InputHandle(GameTime gameTime)
        {
            if ((GesturesInput.GetInstance().GetGestureType() == GestureType.FreeDrag
                || GesturesInput.GetInstance().GetGestureType() == GestureType.HorizontalDrag
                || GesturesInput.GetInstance().GetGestureType() == GestureType.Hold
                || GesturesInput.GetInstance().GetGestureType() == GestureType.VerticalDrag) && (m_gridState == Grid_State.Wait || m_gridState == Grid_State.Touching))
            {
                Vector2 posTap = GesturesInput.GetInstance().GetGesturePosition();
                int tempI = (int)(posTap.Y - EXTRAPOS.Y) / TILE;
                int tempJ = (int)(posTap.X - EXTRAPOS.X) / TILE;

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
                                switch (m_listCell[i].m_type)
                                {
                                    case Cell_Type.Type01:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType1);
                                        break;
                                    case Cell_Type.Type02:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType2);
                                        break;
                                    case Cell_Type.Type03:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType3);
                                        break;
                                    case Cell_Type.Type04:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType4);
                                        break;
                                    case Cell_Type.Type05:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType5);
                                        break;
                                    case Cell_Type.Type06:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType6);
                                        break;
                                    case Cell_Type.Type07:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType7);
                                        break;
                                    case Cell_Type.Type08:
                                        SoundManager.GetInstance().PlaySound(ESound.TouchType8);
                                        break;
                                }
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

            if (GesturesInput.GetInstance().GetGestureType() == GestureType.DragComplete)
            {
                DragCompleteHandle();
            }
            if (m_countFalling == 0 && m_gridState == Grid_State.Falling)
                m_gridState = Grid_State.Wait;
        }

        public void DragCompleteHandle()
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
                        m_gridState = Grid_State.ToTwo;
                    m_countBeExplosion = m_listTouch.Count;
                    m_mainGame.SetPoint(m_currentCellType, 2, m_lastCellPos);
                    SoundManager.GetInstance().PlaySound(ESound.FailCell);
                    
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
                    //CalculatorPoint();
                    m_countBeExplosion = m_listTouch.Count;
                    
                    break;
            }
            if (m_listTouch.Count >= 1)
            {
                randomCell(m_listTouch.Count);
            }

            if (m_listTouch.Count > 0)
                m_countCell = m_listTouch.Count;
            m_listTouch.Clear();
        }
        
        void SwitchTouchingTo()
        {
            SwitchTouchingToThree();
            SwitchTouchingToFour();
            SwitchTouchingToFive();
            SwitchTouchingToGold();
            SwitchTouchingToFinal();
        }
        bool SwitchTouchingToFinal()
        {
            if (m_gridState == Grid_State.Touching
                && m_listTouch.Count >= 3
                && m_currentCellType == Cell_Type.Type08)
            {
                m_gridState = Grid_State.ToFinal;
                return true;
            }
            return false;
        }
        bool SwitchTouchingToThree()
        {
            if (m_gridState == Grid_State.Touching
                && (m_listTouch.Count == 3)
                && (m_currentCellType == Cell_Type.Type01
                || m_currentCellType == Cell_Type.Type02
                || m_currentCellType == Cell_Type.Type03
                || m_currentCellType == Cell_Type.Type04
                || m_currentCellType == Cell_Type.Type05))
            {
                m_gridState = Grid_State.ToThree;
            }
            return false;
        }

        bool SwitchTouchingToFour()
        {
            if (m_gridState == Grid_State.Touching
                && (m_listTouch.Count == 4)
                && (m_currentCellType == Cell_Type.Type01
                || m_currentCellType == Cell_Type.Type02
                || m_currentCellType == Cell_Type.Type03
                || m_currentCellType == Cell_Type.Type04
                || m_currentCellType == Cell_Type.Type05))
            {
                m_gridState = Grid_State.ToFour;
            }
            return false;
        }

        bool SwitchTouchingToFive()
        {
            if (m_gridState == Grid_State.Touching
                && (m_listTouch.Count >= 5)
                && (m_currentCellType == Cell_Type.Type01
                || m_currentCellType == Cell_Type.Type02
                || m_currentCellType == Cell_Type.Type03
                || m_currentCellType == Cell_Type.Type04
                || m_currentCellType == Cell_Type.Type05))
            {
                m_gridState = Grid_State.ToFive;
            }
            return false;
        }

        bool SwitchTouchingToGold()
        {
            if (m_gridState == Grid_State.Touching
                && (m_listTouch.Count >= 3)
                && (m_currentCellType == Cell_Type.Type06
                || m_currentCellType == Cell_Type.Type07))
            {
                m_gridState = Grid_State.ToGold;
            }
            return false;
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
