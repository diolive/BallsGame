using System;

using BallsGame.Control;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallsGame.GameObjects
{
	public class Ball : BallBase
	{
		private readonly Rectangle _clientBounds;
		private readonly Color _color;
		private readonly float _enlargeReduceRate;
		private readonly Color _hoveredColor;
		private readonly float _rotationSpeed;
		private readonly int _size;

		private float _radius;
		private float _scale;
		private Velocity _velocity;

		public Ball(Rectangle clientBounds, ContentManager contentManager)
			: base(contentManager)
		{
			_clientBounds = clientBounds;
			_color = new Color(RandomHelper.NextInt(256), RandomHelper.NextInt(256), RandomHelper.NextInt(256));
			_enlargeReduceRate = 0.002f;
			_hoveredColor = new Color(230, 0, 0);
			_rotationSpeed = 0.003f;
			_size = Math.Max(Texture.Width, Texture.Height);
			
			_scale = 0.1f; //(float) RandomHelper.Instance.NextDouble();
			RecalculateRadius();

			_velocity = new Velocity(0.3f, RandomHelper.NextFloat() * MathHelper.TwoPi);

			Position = new Vector2(RandomHelper.NextInt(10, 400), RandomHelper.NextInt(10, 400));
		}

		public float MoveDirection => _velocity.Direction;

		public BallControl Control { get; set; }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var time = (float) gameTime.ElapsedGameTime.TotalMilliseconds;

			if (Control != null)
			{
				ProcessControls(time);
			}

			Position = Move(time);
		}

		private Vector2 Move(float time)
		{
			Vector2 newPosition = Position + _velocity.GetMovement() * time;
			var boundingBox = new Rectangle(Point.Zero, _clientBounds.Size);

			if (newPosition.X + _radius > boundingBox.Right)
			{
				float percentageBeforeCollide = (boundingBox.Right - Position.X - _radius) / (newPosition.X - Position.X);
				if (percentageBeforeCollide < 0.99)
				{
					Move(percentageBeforeCollide * time);
					time *= 1 - percentageBeforeCollide;
				}

				_velocity.FlipX();
				Move(time);
			}
			else if (newPosition.X - _radius < 0)
			{
				float actualX2 = _radius;
				float actualY2 = Position.Y + (actualX2 - Position.X) / (newPosition.X - Position.X) * (newPosition.Y - Position.Y);

				newPosition = new Vector2(actualX2, actualY2);
				_velocity.FlipX();
			}

			if (newPosition.Y + _radius > _clientBounds.Height)
			{
				float actualY3 = _clientBounds.Height - _radius;
				float actualX3 = Position.X + (actualY3 - Position.Y) / (newPosition.Y - Position.Y) * (newPosition.X - Position.X);

				newPosition = new Vector2(actualX3, actualY3);
				_velocity.FlipY();
			}
			else if (newPosition.Y - _radius < 0)
			{
				float actualY4 = _radius;
				float actualX4 = Position.X + (actualY4 - Position.Y) / (newPosition.Y - Position.Y) * (newPosition.X - Position.X);

				newPosition = new Vector2(actualX4, actualY4);
				_velocity.FlipY();
			}

			return newPosition;
		}

		protected override void DrawVisible(SpriteBatch spriteBatch)
		{
			Color color = ContainsPoint(Mouse.GetState().Position) ? _hoveredColor : _color;
			spriteBatch.Draw(Texture, Position, null, color, 0f, Origin, _scale, SpriteEffects.None, 0);
		}

		private void ProcessControls(float time)
		{
			IControlsState controlsState = new ControlsState();

			if (Control.TurnLeft.IsPressed(controlsState))
			{
				Turn(-time * _rotationSpeed);
			}

			if (Control.TurnRight.IsPressed(controlsState))
			{
				Turn(time * _rotationSpeed);
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
			_velocity.Direction += angle;
		}

		private bool ContainsPoint(Point point)
		{
			return Vector2.Distance(point.ToVector2(), Position) <= _radius;
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
			return Vector2.Distance(Position, ball.Position) <= _radius + ball._radius;
		}

		public void Eat()
		{
			Enlarge(0.1f);
		}
	}
}