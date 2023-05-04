using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Time_and_sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        SpriteFont timeFont;
        Texture2D explosionTexture;
        Rectangle wireRect;
        Rectangle displayRect;
        float seconds;
        float startTime;
        MouseState mouseState;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        bool explosion = false;
        bool disarm = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            timeFont = Content.Load<SpriteFont>("timeFont");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
            wireRect = new Rectangle(425, 225, 75, 15);
            displayRect = new Rectangle(355,225,65,50);
            bombTexture = Content.Load<Texture2D>("bomb");
            explosionTexture = Content.Load<Texture2D>("imgExplosion");
        }

        protected override void Update(GameTime gameTime)
        {
              

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!disarm)
            {
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                if (seconds > 15) // Takes a timestamp every 10 seconds.
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            // TODO: Add your update logic here
           
            
            if (seconds >= 15 && !explosion)
            {
                explodeInstance.Play();
                startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                explosion = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (wireRect.Contains(mouseState.Position))
                {
                    disarm = true;
                }
                else if (displayRect.Contains(mouseState.Position))
                {
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }
            }
            





            if (explodeInstance.State == SoundState.Stopped && explosion)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (!explosion)
            {
                _spriteBatch.Draw(bombTexture, new Rectangle(300, 200, 200, 100), Color.White);
                _spriteBatch.DrawString(timeFont, (15 - seconds).ToString("00.0"), new Vector2(370, 240), Color.Black);
            }
            else if (explosion)
            {
                _spriteBatch.Draw(explosionTexture, new Rectangle(300, 150, 200, 200), Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}