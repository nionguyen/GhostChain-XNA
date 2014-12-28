using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections;

using Microsoft.Xna.Framework.Input;
namespace WindowsPhoneGame2
{
    class Sprite
    {
        private Texture2D m_texture;
        private int m_totalFrame;
        private int m_cols;
        private int m_width;
        private int m_height;
        private Vector2 m_pos;
        private Vector2 m_origin;
        private float m_depth;
        private float m_scale;
        private float m_rotation;
        private SpriteEffects m_effect;
        private Color m_color;
        private Rectangle m_srcRect;
        //private animation m_animation;
        private string m_spriteName;
		
		/// animation
		private float m_interval, m_localTime;
        private int m_firstFrame, m_lastFrame, m_curFrame;
        public int Loop;
        #region get and set function
        public Texture2D Texture
        {
            get { return m_texture; }
            set { m_texture = value; }
        }
        public int Colums
        {
            get{ return m_cols; }
            set{ m_cols = value; }
        }
        public int Width
        {
            get{ return m_width; }
            set{ m_width = value; }
        }
        public int Height
        {
            get{ return m_height; }
            set{ m_height = value; }
        }
        public int TotalFrame
        {
            get{return m_totalFrame;}
            set{m_totalFrame=value;}
        }
        public Vector2 Position
        {
            get { return m_pos; }
            set { m_pos = value; }
        }
        public Vector2 Origin
        {
            get { return m_origin; }
            set { m_origin = value; }
        }
        public float Depth
        {
            get { return m_depth; }
            set { m_depth = value; }
        }
        public float Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }
        public float Rotation
        {
            get { return m_rotation; }
            set { m_rotation = value; }
        }
        public SpriteEffects Effect
        {
            get { return m_effect; }
            set { m_effect = value; }
        }
        public Color Color
        {
            get { return m_color; }
            set { m_color = value; }
        }
        public Rectangle SrcRect
        {
            get { return m_srcRect; }
            set { m_srcRect = value; }
        }
        //public animation Animation
        //{
        //    get { return m_animation; }
        //    set { m_animation = value; }
        //}
        public string SpriteName
        {
            get { return m_spriteName; }
            set { m_spriteName = value; }
        }
		///
		public int FirstFrame
        {
            get { return m_firstFrame; }
            set { m_firstFrame = value; }
        }
        public int LastFrame
        {
            get { return m_lastFrame; }
            set { m_lastFrame = value; }
        }
        public int CurentFrame
        {
            get { return m_curFrame; }
            set { m_curFrame = value; }
        }
        public float Interval
        {
            get { return m_interval; }
            set { m_interval = value; }
        }
        public float Localtime
        {
            get { return m_localTime; }
            set { m_localTime = value; }
        }
        #endregion

        public Sprite(string _spriteName, int _totalFrame, int _cols, int _width, int _height, int _interval)//them _interval
        {
            m_spriteName = _spriteName;
            m_texture = null;
            m_totalFrame = _totalFrame;
            m_cols = _cols;
            m_height = _height;
            m_width = _width;
            m_pos = Vector2.Zero;
            m_origin = Vector2.Zero;
            m_rotation = 0.0f;
            m_depth = 0.1f;
            m_scale = 1.0f;
            m_color = Color.White;
            m_effect = SpriteEffects.None;
            //m_animation = new animation();
            m_srcRect = new Rectangle(0, 0, m_width, m_height);
			///
			m_interval = _interval;
            m_lastFrame = m_totalFrame - 1;
			m_firstFrame = 0;
            m_curFrame = 0;
            m_localTime = 0.0f;
			

        }
        public Sprite(Sprite _copy)
        {
            m_texture = _copy.m_texture;
            m_cols = _copy.m_cols;
            m_width = _copy.m_width;
            m_height = _copy.m_height;
            m_totalFrame = _copy.m_totalFrame;
            m_pos = _copy.m_pos;
            m_srcRect = _copy.m_srcRect;
            m_color = _copy.m_color;
            m_rotation = 0.0f;
            m_origin = Vector2.Zero;
            m_scale = _copy.m_scale;
            m_depth = _copy.m_depth;
            //m_animation = new animation(_copy.m_animation);
            m_spriteName = _copy.m_spriteName;
			
			///
			m_interval = _copy.m_interval;
            m_lastFrame = _copy.m_totalFrame - 1;
			m_firstFrame = 0;
            m_curFrame = 0;
            m_localTime = 0.0f;
        }
		//public void LoadContent(ContentManager theContentManager, int _firstFrame,int _lastFrame, int _interval)//
        //{
        //   
        //    m_texture = theContentManager.Load<Texture2D>(m_spriteName);
        //    m_animation.CreateAnimation(_firstFrame, _lastFrame, _interval);//
        //}
        public void LoadContent(ContentManager theContentManager)//
        {
            
            m_texture = theContentManager.Load<Texture2D>(m_spriteName);
            //m_animation.CreateAnimation(_firstFrame, _lastFrame, m_interval);//k can thiet nua
        }
        public void Update(GameTime _gametime)
        {
            if (m_interval == 0)
                return;
            //m_animation.UpdateAnimation(_gametime, ref m_srcRect, m_width, m_width,m_cols);
            //(GameTime _Gametime, ref Rectangle _srcRect, int _Width, int _Height, int _Cols)
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            m_localTime += (float)_gametime.ElapsedGameTime.TotalMilliseconds;
            if (m_localTime >= m_interval)
            {
                //nextframe();
				m_curFrame++;
                if (m_curFrame > m_lastFrame)
                {
                    Loop++;
                    m_curFrame = m_firstFrame;
                }

                m_localTime -= m_interval;
                rect.X = (m_curFrame % m_cols) * m_width;
                rect.Y = (m_curFrame / m_cols) * m_width;
                rect.Width = m_width;
                rect.Height = m_width;
                m_srcRect = rect;
            }
        }
        
        public void Update(GameTime _gametime, int _curFrame)
        {
            m_srcRect.X = (_curFrame % m_cols) * m_width;
            m_srcRect.Y = (_curFrame / m_cols) * m_height;
            m_srcRect.Width = m_width;
            m_srcRect.Height = m_height;
        }
        public void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(m_texture, m_pos, m_srcRect, m_color, m_rotation, m_origin, m_scale, m_effect, m_depth);
        }
        public void Draw(SpriteBatch theSpriteBatch, Rectangle _srcRect)
        {
            theSpriteBatch.Draw(m_texture, m_pos, _srcRect, m_color, m_rotation, m_origin, m_scale, m_effect, m_depth);
            
        }
        public void Draw(SpriteBatch theSpriteBatch, Vector2 _pos)
        {
            theSpriteBatch.Draw(m_texture, _pos, m_srcRect, m_color, m_rotation, m_origin, m_scale, m_effect, m_depth);
        }





    }
}
