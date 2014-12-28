using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace WindowsPhoneGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
            
            //SoundManager.LoadContent(Content);
            SoundManager.GetInstance().LoadContent(Content);
            
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            screenManager.AddScreen(new MenuScreen());
            graphics.IsFullScreen = true;
        }


        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.DragComplete |
                             GestureType.FreeDrag |
                             GestureType.HorizontalDrag |
                             GestureType.Tap | GestureType.VerticalDrag;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ResourceManager.GetInstance().LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                Global.Back = true;
            }
            else
            {
                Global.Back = false;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
        }
    }
}
