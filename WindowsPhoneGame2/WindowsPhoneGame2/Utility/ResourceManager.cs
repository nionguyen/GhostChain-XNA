using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace WindowsPhoneGame2
{
    class ResourceManager
    {
        List<Sprite> m_spriteList = new List<Sprite>();
        private static ResourceManager s_pInstance = null;
        
        private ResourceManager()
        {
            m_spriteList.Add(new Sprite("Cell/Type01_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type02_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type03_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type04_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type05_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type06_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type07_Touching", 2, 2, 80, 80, 400));
            m_spriteList.Add(new Sprite("Cell/Type08_Touching", 2, 2, 80, 80, 400));

            m_spriteList.Add(new Sprite("Cell/Type01_NoTouch", 2, 2, 80, 80, 1000));
            m_spriteList.Add(new Sprite("Cell/Type02_NoTouch", 2, 2, 80, 80, 600));
            m_spriteList.Add(new Sprite("Cell/Type03_NoTouch", 2, 2, 80, 80, 300));
            m_spriteList.Add(new Sprite("Cell/Type04_NoTouch", 2, 2, 80, 80, 550));
            m_spriteList.Add(new Sprite("Cell/Type05_NoTouch", 2, 2, 80, 80, 500));
            m_spriteList.Add(new Sprite("Cell/Type06_NoTouch", 2, 2, 80, 80, 800));
            m_spriteList.Add(new Sprite("Cell/Type07_NoTouch", 2, 2, 80, 80, 800));
            m_spriteList.Add(new Sprite("Cell/Type08_NoTouch", 2, 2, 80, 80, 300));

            m_spriteList.Add(new Sprite("Cell/Type01_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type02_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type03_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type04_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type05_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type06_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type07_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type08_Action", 1, 1, 80, 80, 0));

            m_spriteList.Add(new Sprite("Cell/Explosion", 4, 4, 251, 251, 120));

            m_spriteList.Add(new Sprite("Blank_Game", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Game", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("PausePopup", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("Music_Off", 1, 1, 62, 62, 0));
            m_spriteList.Add(new Sprite("BG_Level", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Level_Select", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Level_Mode_Done", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Classic_Mode_Done", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_How_To_Play", 3, 3, 382, 511, 0));
            m_spriteList.Add(new Sprite("Current_Level_Grid", 1, 1, 47, 46, 0));
            m_spriteList.Add(new Sprite("Level_Locked", 1, 1, 130, 139, 0));
            m_spriteList.Add(new Sprite("Complete", 1, 1, 262, 78, 0));
            m_spriteList.Add(new Sprite("Star", 1, 1, 24, 24, 0));
            m_spriteList.Add(new Sprite("High_Score", 1, 1, 95, 49, 0));

            //Chinese
            /*
            m_spriteList.Add(new Sprite("Blank_Game", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Game", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("PausePopup", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("Music_Off", 1, 1, 62, 62, 0));
            m_spriteList.Add(new Sprite("BG_Level", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Level_Select", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Level_Mode_Done", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_Classic_Mode_Done", 1, 1, 480, 800, 0));
            m_spriteList.Add(new Sprite("BG_How_To_Play", 3, 3, 382, 511, 0));
            m_spriteList.Add(new Sprite("Current_Level_Grid", 1, 1, 47, 46, 0));
            m_spriteList.Add(new Sprite("Level_Locked", 1, 1, 130, 139, 0));
            m_spriteList.Add(new Sprite("Complete", 1, 1, 267, 78, 0)
            m_spriteList.Add(new Sprite("High_Score", 1, 1, 119, 37, 0));
             */
        }

        public void LoadContent(ContentManager _contentManager)
        {
            //int _lastFrame;
            //int _interval = 150;
            for (int i = 0; i < m_spriteList.Count(); i++)
            {
                //_lastFrame = m_spriteList[i].TotalFrame - 1;
                m_spriteList[i].LoadContent(_contentManager);//bo bot _interval, _lastFrame, _first frame (tu tinh)
            }
        }

        public static ResourceManager GetInstance()
        {
            if (s_pInstance == null)
            {
                s_pInstance = new ResourceManager();
                return s_pInstance;
            }
            else
                return s_pInstance;
        }

        public Sprite GetSprite(String _spriteName)
        {
            for (int i = 0; i < m_spriteList.Count(); i++)
            {
                if (String.Compare(m_spriteList[i].SpriteName.ToString(), _spriteName.ToString()) == 0)
                {
                    return m_spriteList[i];
                }
            }
            return null;
        }

    }
}
