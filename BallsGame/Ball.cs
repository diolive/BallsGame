using System;

using BallsGame.Control;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallsGame
{
	public class Ball : GameObject
	{
		private Color _color;
		private SpriteEffects _effects;
		private Color _hoveredColor;
		private Direction _moveDirection;
		private Vector2 _origin;
		private Vector2 _position;
		private float _radius;
		private float _rotation;
		private float _scale;
		private int _size;
		private Texture2D _texture;
		private float _velocity;

		private float _turnVelocity;
		private float _enlargeReduceRate;

		public Ball(Game game)
			: base(game)
		{
		}

		public float MoveDirection => _moveDirection.Angle;
		public Vector2 Position => _position;

		public BallControl Control { get; set; }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			if (Control != null)
			{
				ProcessControls(time);
			}

			_position = Move(time);
		}

		private Vector2 Move(float time)
		{
			Vector2 newPosition = _position + Vector2.Transform(Vector2.UnitX * (time * _velocity), Matrix.CreateRotationZ(_moveDirection.Angle));
			var boundingBox = new Rectangle(Point.Zero, Game.Window.ClientBounds.Size);

			if (newPosition.X + _radius > boundingBox.Right)
			{
				float percentageBeforeCollide = (boundingBox.Right - _position.X - _radius) / (newPosition.X - _position.X);
				if (percentageBeforeCollide < 0.99)
				{
					Move(percentageBeforeCollide * time);
					time -= percentageBeforeCollide * time;
				}

				_moveDirection.FlipX();
				Move(time);
			}
			else if (newPosition.X - _radius < 0)
			{
				float actualX2 = _radius;
				float actualY2 = _position.Y + (actualX2 - _position.X) / (newPosition.X - _position.X) * (newPosition.Y - _position.Y);

				newPosition = new Vector2(actualX2, actualY2);
				_moveDirection.FlipX();
			}

			if (newPosition.Y + _radius > Game.Window.ClientBounds.Height)
			{
				float actualY3 = Game.Window.ClientBounds.Height - _radius;
				float actualX3 = _position.X + (actualY3 - _position.Y) / (newPosition.Y - _position.Y) * (newPosition.X - _position.X);

				newPosition = new Vector2(actualX3, actualY3);
				_moveDirection.FlipY();
			}
			else if (newPosition.Y - _radius < 0)
			{
				float actualY4 = _radius;
				float actualX4 = _position.X + (actualY4 - _position.Y) / (newPosition.Y - _position.Y) * (newPosition.X - _position.X);

				newPosition = new Vector2(actualX4, actualY4);
				_moveDirection.FlipY();
			}

			return newPosition;
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			Color color = ContainsPoint(Mouse.GetState().Position) ? _hoveredColor : _color;
			SpriteBatch.Draw(_texture, _position, null, color, _rotation, _origin, _scale, _effects, 0);
		}

		protected override void LoadContent()
		{
			_texture = Game.Content.Load<Texture2D>(@"ball");
			_size = Math.Max(_texture.Width, _texture.Height);
			_color = new Color(RandomHelper.Instance.Next(256), RandomHelper.Instance.Next(256), RandomHelper.Instance.Next(256));
			_hoveredColor = new Color(230, 0, 0);
			_effects = SpriteEffects.None;
			_position = new Vector2(RandomHelper.Instance.Next(10, 400), RandomHelper.Instance.Next(10, 400));
			_rotation = 0f;
			_scale = 0.1f; //(float) RandomHelper.Instance.NextDouble();
			_origin = _texture.Bounds.Size.ToVector2() / 2f;
			_velocity = 0.3f;
			_moveDirection = new Direction((float)(RandomHelper.Instance.NextDouble() * MathHelper.TwoPi));
			RecalculateRadius();
			_turnVelocity = 0.003f;
			_enlargeReduceRate = 0.002f;
		}

		private void ProcessControls(float time)
		{
			IControlsState controlsState = new ControlsState();

			if (Control.TurnLeft.IsPressed(controlsState))
			{
				Turn(-time * _turnVelocity);
			}

			if (Control.TurnRight.IsPressed(controlsState))
			{
				Turn(time * _turnVelocity);
			}

			if (Control.Enlarge.IsPressed(controlsState))
			{
				Enlarge(time * _enlargeReduceRate);
			}

			if (Control.Reduce.IsPressed(controlsState))
			{
				Reduce(time * _enlargeReduceRate);
			}
		}

		private void Turn(float angle)
		{
			_moveDirection.Angle += angle;
		}

		private bool ContainsPoint(Point point)
		{
			return Vector2.Distance(point.ToVector2(), _position) <= _radius;
		}

		private void Enlarge(float enlargeRate)
		{
			if (enlargeRate <= 0)
			{
				throw new ArgumentException();
			}

			_scale = Math.Min(_scale * (1 + enlargeRate), 3.5f);
			RecalculateRadius();
		}

		private void Reduce(float reduceRate)
		{
			if (reduceRate <= 0)
			{
				throw new ArgumentException();
			}

			_scale = Math.Max(_scale / (1 + reduceRate), 0.1f);
			RecalculateRadius();
		}

		private void RecalculateRadius()
		{
			_radius = _size / 2f * _scale;
		}

		public bool IntersectsWith(Ball ball)
		{
			return Vector2.Distance(_position, ball._position) <= _radius + ball._radius;
		}

		public void Eat()
		{
			Enlarge(0.1f);
		}
	}
}