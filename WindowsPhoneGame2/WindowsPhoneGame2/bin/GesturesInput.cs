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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GesturesInput
    {
        List<GestureSample> gestures = new List<GestureSample>();
        SpriteFont spriteFont;
        public GesturesInput()
        {
           
        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("SpriteFont1");
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

        public bool isGesture()
        {
            if (gestures.Count > 0)
                return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 textPosition = new Vector2(5, 5);

            // Loop over all of the gestures and draw their information to the screen
            for (int i = gestures.Count - 1; i >= 0; i--)
            {
                // Draw all of the properties of the gesture
                GestureSample gesture = gestures[i];
                spriteBatch.DrawString(spriteFont, "GestureType: " + gesture.GestureType.ToString(), textPosition, Color.White);
                textPosition.Y += 25;
                spriteBatch.DrawString(spriteFont, "Position: " + gesture.Position.ToString(), textPosition, Color.White);
                textPosition.Y += 25;
                spriteBatch.DrawString(spriteFont, "Position2: " + gesture.Position2.ToString(), textPosition, Color.White);
                textPosition.Y += 25;
                spriteBatch.DrawString(spriteFont, "Delta: " + gesture.Delta.ToString(), textPosition, Color.White);
                textPosition.Y += 25;
                spriteBatch.DrawString(spriteFont, "Delta2: " + gesture.Delta2.ToString(), textPosition, Color.White);
                textPosition.Y += 25;
                spriteBatch.DrawString(spriteFont, "Timestamp: " + gesture.Timestamp.ToString(), textPosition, Color.White);
                textPosition.Y += 40;
            }
        }
      
    }
}
