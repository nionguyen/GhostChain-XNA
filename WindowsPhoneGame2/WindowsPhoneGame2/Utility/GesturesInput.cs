using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;


namespace WindowsPhoneGame2
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GesturesInput
    {
        public List<GestureSample> gestures = new List<GestureSample>();
        private static GesturesInput s_pInstance = null;
        private GesturesInput()
        {

        }

        public static GesturesInput GetInstance()
        {
            if (s_pInstance == null)
            {
                s_pInstance = new GesturesInput();
                return s_pInstance;
            }
            else
                return s_pInstance;
        }

        public void Update(GameTime gameTime)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                // Add each to the stack
                gestures.Add(TouchPanel.ReadGesture());
            }
            if (gestures.Count > 5)
                gestures.RemoveRange(0, gestures.Count - 5);
        }

        public GestureType GetGestureType()
        {
            if (gestures.Count > 0)
            {
                GestureSample gesture = gestures[gestures.Count - 1];
                return gesture.GestureType;
            }
            return GestureType.None;
        }

        public Vector2 GetGesturePosition()
        {
            if (gestures.Count > 0)
            {
                GestureSample gesture = gestures[gestures.Count - 1];
                return gesture.Position;
            }
            return Vector2.Zero;
        }

        public Vector2 GetGestureDetla()
        {
            if (gestures.Count > 0)
            {
                GestureSample gesture = gestures[gestures.Count - 1];
                return gesture.Delta;
            }
            return Vector2.Zero;
        }

        public void Reset()
        {
            gestures.Clear();
        }

        public Point GetGesturePoint()
        {
            if (gestures.Count > 0)
            {
                GestureSample gesture = gestures[gestures.Count - 1];
                Point posTap = new Point((int)gesture.Position.X, (int)gesture.Position.Y);
                return posTap;
            }
            return new Point(0, 0);
        }


        public bool isGesture()
        {
            if (gestures.Count > 0)
                return true;
            return false;
        }

        public DirDrag GetDirDrag()
        {
            if (gestures.Count <= 0)
                return DirDrag.None;
            if (gestures[gestures.Count - 1].GestureType == GestureType.HorizontalDrag)
            {
                if (gestures[gestures.Count - 1].Delta.X > 0)
                {
                    return DirDrag.Left_Right;
                }

                //Right _ Left
                if (gestures[gestures.Count - 1].Delta.X < 0)
                {
                    return DirDrag.Right_Left;
                }
            }
            if (gestures[gestures.Count - 1].GestureType == GestureType.VerticalDrag)
            {
                //Top _ Bot
                if (gestures[gestures.Count - 1].Delta.Y > 0)
                {
                    return DirDrag.Top_Bot;
                }

                //Right _ Left
                if (gestures[gestures.Count - 1].Delta.Y < 0)
                {
                    return DirDrag.Bot_Top;
                }
            }
            return DirDrag.None;
        }
    }
}
