using System.Collections.Generic;
using System.Linq;

using BallsGame.Control;
using BallsGame.GameObjects;

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
		private List<GameObject> _gameObjects;

		private GraphicsDeviceManager _graphics;
		private Spot _spot;
		private SpriteBatch _spriteBatch;
		private Ball _targetBall1;
		private Ball _targetBall2;
		private List<Wall> _walls;

		public TheGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

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

			//Window.IsBorderless = true;
		}

		/// <summary>
		///     LoadContent will be called once per game and is the place to load
		///     all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			_backgroundColor = new Color(20, 20, 20);

			_gameObjects = new List<GameObject>();

			_targetBall1 = new Ball(Window.ClientBounds, Content)
			{
				Control = new BallControl
				{
					TurnLeft = new KeyboardControl(Keys.A),
					TurnRight = new KeyboardControl(Keys.D),
					Enlarge = new KeyboardControl(Keys.W),
					Reduce = new KeyboardControl(Keys.S)
				}
			};
			_gameObjects.Add(_targetBall1);

			_targetBall2 = new Ball(Window.ClientBounds, Content)
			{
				Control = new BallControl
				{
					TurnLeft = new KeyboardControl(Keys.NumPad4),
					TurnRight = new KeyboardControl(Keys.NumPad6),
					Enlarge = new KeyboardControl(Keys.NumPad8),
					Reduce = new KeyboardControl(Keys.NumPad2)
				}
			};
			_gameObjects.Add(_targetBall2);

			for (var i = 0; i < 100; i++)
			{
				_gameObjects.Add(new Ball(Window.ClientBounds, Content));
			}

			int bottom = Window.ClientBounds.Bottom;
			int right = Window.ClientBounds.Right;

			_walls = new List<Wall>
			{
				new Wall(Vector2.Zero, new Vector2(0, bottom)),
				new Wall(new Vector2(0, bottom), new Vector2(right, bottom)),
				new Wall(Vector2.Zero, new Vector2(right, 0)),
				new Wall(new Vector2(right, 0), new Vector2(right, bottom))
			};

			_gameObjects.Add(new Cursor(Content));

			_spot = new Spot(Content);
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
				_gameObjects.Add(new Ball(Window.ClientBounds, Content));
			}

			// TODO: Add your update logic here
			List<Ball> eatenBalls = _gameObjects
				.OfType<Ball>()
				.Where(ball => ball != _targetBall1 && ball.IntersectsWith(_targetBall1))
				.ToList();

			foreach (Ball eatenBall in eatenBalls)
			{
				_targetBall1.Eat();
				_gameObjects.Remove(eatenBall);
			}

			_spot.Visible = false;

			foreach (Vector2? i in _walls
				.Select(wall => wall.Intersection(_targetBall1.Position, _targetBall1.MoveDirection))
				.Where(i => i.HasValue))
			{
				_spot.Visible = true;
				_spot.Position = i.Value;
			}

			foreach (GameObject gameObject in _gameObjects)
			{
				gameObject.Update(gameTime);
			}
		}

		/// <summary>
		///     This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(_backgroundColor);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();
			foreach (GameObject gameObject in _gameObjects)
			{
				gameObject.Draw(_spriteBatch);
			}

			_spriteBatch.End();
		}
	}
}