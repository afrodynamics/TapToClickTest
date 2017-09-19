using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TapToClickTest
{
    public class TestGame : Game
    {

        GraphicsDeviceManager gdm;
        SpriteBatch sb;
        List<LameSprite> spriteList;
        static Texture2D whiteTex;

        class LameSprite {

            int x, y;
            Color color = Color.Red;
            public bool dead = false;

            public LameSprite(int x, int y, Color c) {
                this.x = x;
                this.y = y;
                color = c;
            }

            public void Draw(SpriteBatch batch) {
                if (dead)
                    return;

                batch.Draw(whiteTex, new Rectangle(x - 5, y - 5, 10, 10), null, color);
            }
        }

        public TestGame()
        {
			gdm = new GraphicsDeviceManager(this);
            spriteList = new List<LameSprite>();
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            sb = new SpriteBatch(GraphicsDevice);
            whiteTex = new Texture2D(GraphicsDevice, 1, 1);
            whiteTex.SetData<Color>(new Color[] {
                Color.White
            });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            whiteTex.Dispose();
        }

        private MouseState prevState;
        private MouseState nextState;

        protected override void Update(GameTime gameTime)
        {
            float dt = gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            base.Update(gameTime);

            // Swap mouse states
            prevState = nextState;
            nextState = Mouse.GetState();

            if (prevState == null)
                return;

            if (nextState.LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released) {
                spriteList.Add(new LameSprite(nextState.X, nextState.Y, Color.Purple));
            }
            else if (nextState.LeftButton == ButtonState.Pressed) 
            {
                spriteList.Add(new LameSprite(nextState.X, nextState.Y, Color.Red));
            }

            var kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Space)) {
                spriteList.Clear();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.White);

			var removeList = new List<LameSprite>();

            foreach (var s in spriteList) {
                sb.Begin();
                s.Draw(sb);
				sb.End();
            }
        }
    }
}
