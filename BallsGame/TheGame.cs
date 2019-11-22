using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallsGame
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class TheGame : Game
    {
        private Color _backgroundColor;
        private Ball _targetBall1;
        private Ball _targetBall2;

        public TheGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public GraphicsDeviceManager Graphics { get; }
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _targetBall1 = AddComponent<Ball>()
                .Apply(ball =>
                {
                    ball.Control = new BallControl
                    {
                        TurnLeft = Keys.A,
                        TurnRight = Keys.D,
                        Enlarge = Keys.W,
                        Reduce = Keys.S
                    };
                });

            //_targetBall2 = AddComponent<Ball>()
            //    .Apply(ball =>
            //    {
            //        ball.Control = new BallControl
            //        {
            //            TurnLeft = Keys.NumPad4,
            //            TurnRight = Keys.NumPad6,
            //            Enlarge = Keys.NumPad8,
            //            Reduce = Keys.NumPad2
            //        };
            //    });

            for (var i = 0; i < 100; i++)
            {
                AddComponent<Ball>();
            }

            AddComponent<Cursor>();
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _backgroundColor = new Color(20, 20, 20);
        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                AddComponent<Ball>();
            }

            // TODO: Add your update logic here
            var eatenBalls = new List<Ball>();

            foreach (Ball ball in Components.OfType<Ball>())
            {
                if (ball != _targetBall1 && ball.IntersectsWith(_targetBall1))
                {
                    eatenBalls.Add(ball);
                    _targetBall1.Eat();
                }
            }

            foreach (Ball eatenBall in eatenBalls)
            {
                Components.Remove(eatenBall);
            }

            base.Update(gameTime);
        }

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColor);

            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }

        private T AddComponent<T>()
            where T : IGameComponent
        {
            ConstructorInfo ctor = typeof(T).GetConstructor(new[] { typeof(Game) })
                                   ?? throw new ArgumentException("GameObject should contain public constructor receiving Game object");

            var gameObject = (T) ctor.Invoke(new object[] { this });

            Components.Add(gameObject);
            gameObject.Initialize();

            return gameObject;
        }
    }
}