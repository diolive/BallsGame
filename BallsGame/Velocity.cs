using System;

using Microsoft.Xna.Framework;

namespace BallsGame
{
	public struct Velocity
	{
		private float _direction;

		public Velocity(float speed, float direction)
			: this()
		{
			Speed = speed;
			Direction = direction;
		}

		public float Speed { get; set; }

		public float Direction
		{
			get => _direction;
			set => _direction = value % MathHelper.TwoPi;
		}

		public void FlipX()
		{
			Flip(0);
		}

		public void FlipY()
		{
			Flip(MathHelper.PiOver2);
		}

		public void Flip(float wallAngle)
		{
			_direction = (float) Math.Atan2((float) Math.Sin(_direction - wallAngle), (float) -Math.Cos(_direction - wallAngle)) + wallAngle;
		}

		public Vector2 GetMovement()
		{
			return Vector2.Transform(Vector2.UnitX * Speed, Matrix.CreateRotationZ(Direction));
		}
	}
}