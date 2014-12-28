using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame2
{
    class ResourceManager
    {
        List<Sprite> m_spriteList = new List<Sprite>();
        private static ResourceManager s_pInstance = null;
        
        private ResourceManager()
        {
            m_spriteList.Add(new Sprite("Cell/Type01_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type02_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type03_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type04_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type05_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type06_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type07_Touching", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type08_Touching", 2, 2, 80, 80, 200));

            m_spriteList.Add(new Sprite("Cell/Type01_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type02_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type03_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type04_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type05_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type06_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type07_NoTouch", 2, 2, 80, 80, 200));
            m_spriteList.Add(new Sprite("Cell/Type08_NoTouch", 2, 2, 80, 80, 200));

            m_spriteList.Add(new Sprite("Cell/Type01_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type02_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type03_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type04_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type05_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type06_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type07_Action", 1, 1, 80, 80, 0));
            m_spriteList.Add(new Sprite("Cell/Type08_Action", 1, 1, 80, 80, 0));

            m_spriteList.Add(new Sprite("Cell/Explosion", 4, 4, 251, 251, 100));

            m_spriteList.Add(new Sprite("BlankScreen", 1, 1, 480, 800, 0));
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
