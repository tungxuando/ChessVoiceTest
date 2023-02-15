using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Velentr.Input.Conditions;
using Velentr.Input.GamePad;
using Velentr.Input.Keyboard;
using Velentr.Input.Mouse;
using Velentr.Input;
using Velentr.Input.EventArguments;
using Velentr.Input.Voice.SystemSpeech;
using Velentr.Input.Voice;

namespace ChessXAML
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputManager manager;

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

            manager = new InputManager(this);
            manager.Setup();
            manager.SetVoiceService(new DefaultVoiceService(manager), typeof(SystemSpeechEngine), true);
            var condition = new AnyCondition(
                manager,
                new KeyboardButtonPressedCondition(manager, Key.Escape),
                new GamePadButtonPressedCondition(manager, GamePadButton.Back),
                new MouseButtonPressedCondition(manager, MouseButton.MiddleButton),
                new VoiceCommandCondition(manager,"exit")
            );
            condition.Event += ExitGame;
            manager.AddInputConditionToTracking(condition);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            manager.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
           

            base.Draw(gameTime);
        }

        public void ExitGame(object sender, ConditionEventArguments args)
        {
            Exit();
        }

    }
}
